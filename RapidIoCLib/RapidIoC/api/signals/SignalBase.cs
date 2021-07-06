using System;
using System.Collections.Generic;
using System.Linq;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Signals are a way to execute commands.
    /// To use, connect your commands to a signal and the call signal.Dispatch(parameters) to execute connected commands.
    /// </summary>
    public abstract class SignalBase
    {
        #region Nested type: CommandData
        public class CommandData
        {
            #region Properties
            public IBaseCommand Command { get; }
            public bool Once { get; }
            #endregion

            #region Constructors
            public CommandData(IBaseCommand command, bool once)
            {
                Command = command;
                Once = once;
            }
            #endregion
        }
        #endregion

        #region Fields
        private readonly Dictionary<IKey, CommandData> _commands = new Dictionary<IKey, CommandData>();
        private readonly Dictionary<IKey, CommandData> _commandsToAdd = new Dictionary<IKey, CommandData>();
        private readonly HashSet<IKey> _commandsToRemove = new HashSet<IKey>();
        private readonly UidGenerator _uidGenerator = new UidGenerator();
        private bool _dispatching;
        protected internal readonly object _syncRoot = new object();
        #endregion

        #region Properties
        public IEnumerable<KeyValuePair<IKey, CommandData>> Commands => _commands;
        public int CommandCount => _commands.Count;
        #endregion

        #region Methods
        public bool IsScheduledForRemoval(IKey key)
        {
            return _commandsToRemove.Contains(key);
        }

        public bool HasKey(object keyData)
        {
            lock (_syncRoot)
            {
                return Rapid.KeyFactoryCollection.Create(keyData, out var key, out _) && (_commands.ContainsKey(key) || _commandsToAdd.ContainsKey(key));
            }
        }

        public void RemoveCommand(object keyData, bool silent = false)
        {
            if (!Rapid.KeyFactoryCollection.Create(keyData, out var key, out var errorMessage) ||
                !RemoveCommandInternal(key, silent, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public void RemoveCommand<TCommand>() where TCommand : IBaseCommand
        {
            RemoveCommand(typeof(TCommand));
        }

        protected bool RemoveCommandInternal(IKey key, bool silent, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (!_commands.TryGetValue(key, out var commandData))
                {
                    if (silent)
                    {
                        errorMessage = string.Empty;
                        return true;
                    }
                    errorMessage = string.Format("Command with key <{0}> not found.", key);
                    return false;
                }
                if (_dispatching)
                {
                    if (!_commandsToRemove.Add(key))
                    {
                        if (silent)
                        {
                            errorMessage = string.Empty;
                            return true;
                        }
                        errorMessage = string.Format("Command with key <{0}> is already scheduled for removal.", key);
                        return false;
                    }
                    errorMessage = string.Empty;
                    return true;
                }
                commandData.Command.Release();
                if (key is UidKey uidKey)
                {
                    _uidGenerator.RemoveUid(uidKey.Uid);
                }
                _commands.Remove(key);
            }
            errorMessage = string.Empty;
            return true;
        }

        public void ClearCommands()
        {
            lock (_syncRoot)
            {
                while (_commands.Count > 0)
                {
                    if (!RemoveCommandInternal(_commands.Keys.First(), false, out var errorMessage))
                    {
                        throw new Exception(errorMessage);
                    }
                }
            }
        }

        private bool ValidateKey(IKey key, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (_commands.ContainsKey(key))
                {
                    errorMessage = string.Format("Command with key <{0}> already added.", key);
                    return false;
                }
                if (_commandsToRemove.Contains(key))
                {
                    errorMessage = string.Format("Command with key <{0}> is already scheduled for removal.", key);
                    return false;
                }
                if (_commandsToAdd.ContainsKey(key))
                {
                    errorMessage = string.Format("Command with key <{0}> is already scheduled to add.", key);
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }

        protected IKey AddCommandInternal<TCommand>(bool once) where TCommand : IBaseCommand
        {
            if (!Rapid.KeyFactoryCollection.Create(typeof(TCommand), out var key, out var errorMessage) ||
                !AddCommandInternal((IBaseCommand)Activator.CreateInstance(((TypeKey)key).Type), key, once, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
            return key;
        }

        protected IKey AddCommandInternal(IBaseCommand command, object keyData, bool once)
        {
            keyData = keyData ?? _uidGenerator;
            if (!Rapid.KeyFactoryCollection.Create(keyData, out var key, out var errorMessage) ||
                !AddCommandInternal(command, key, once, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
            return key;
        }

        private bool AddCommandInternal(IBaseCommand command, IKey key, bool once, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (!ValidateKey(key, out errorMessage))
                {
                    return false;
                }
                var commandData = new CommandData(command, once);
                var commands = _dispatching ? _commandsToAdd : _commands;
                commands.Add(key, commandData);
                command.Connect();
            }
            errorMessage = string.Empty;
            return true;
        }

        protected bool DispatchBegin()
        {
            if (_dispatching)
            {
                return false;
            }
            _dispatching = true;
            return true;
        }

        protected void DispatchEnd()
        {
            lock (_syncRoot)
            {
                foreach (var kvp in Commands)
                {
                    if (kvp.Value.Once)
                    {
                        _commandsToRemove.Add(kvp.Key);
                    }
                }
                foreach (var key in _commandsToRemove)
                {
                    var command = _commands[key];
                    command.Command.Release();
                    _commands.Remove(key);
                }
                _commandsToRemove.Clear();

                foreach (var kvp in _commandsToAdd)
                {
                    _commands.Add(kvp.Key, kvp.Value);
                }
                _commandsToAdd.Clear();
                _dispatching = false;
            }
        }
        #endregion
    }

    public abstract class SignalBaseResult<T_Result> : SignalBase
    {
        #region Properties
        public virtual T_Result DefaultResult => default;
        public virtual T_Result TargetResult => default;
        public virtual bool StopOnResult => false;
        #endregion

        #region Methods
        public abstract bool ResultEquals(T_Result a, T_Result b);

        public abstract T_Result ResultAggregate(T_Result a, T_Result b);

        protected new void DispatchBegin()
        {
            if (!base.DispatchBegin())
            {
                throw new Exception($"{GetType().Name} is already dispatching, recursive execution for this Signal type is not supported.");
            }
        }
        #endregion
    }
}