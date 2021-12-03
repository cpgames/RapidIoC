using System;
using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    /// <summary>
    /// Signals are a way to execute commands.
    /// To use, connect your commands to a signal and the call signal.Dispatch(parameters) to execute connected commands.
    /// </summary>
    public abstract class SignalBase : ISignalBase
    {
        #region Fields
        private readonly Dictionary<IKey, SignalCommandModel> _commands = new Dictionary<IKey, SignalCommandModel>();
        private readonly Dictionary<IKey, SignalCommandModel> _commandsToAdd = new Dictionary<IKey, SignalCommandModel>();
        private readonly HashSet<IKey> _commandsToRemove = new HashSet<IKey>();
        private UidGenerator _uidGenerator;
        private bool _dispatching;
        protected internal readonly object _syncRoot = new object();
        #endregion

        #region Properties
        private UidGenerator UidGenerator => _uidGenerator ?? (_uidGenerator = new UidGenerator());
        #endregion

        #region ISignalBase Members
        public IEnumerable<KeyValuePair<IKey, SignalCommandModel>> Commands => _commands;
        public int CommandCount => _commands.Count;

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

        public void RemoveCommand<TCommand>(bool silent = false) where TCommand : IBaseCommand
        {
            RemoveCommand(typeof(TCommand), silent);
        }
        #endregion

        #region Methods
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
                    errorMessage = $"Command with key <{key}> not found.";
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
                        errorMessage = $"Command with key <{key}> is already scheduled for removal.";
                        return false;
                    }
                    errorMessage = string.Empty;
                    return true;
                }
                if (!commandData.Command.Release(out errorMessage))
                {
                    return false;
                }
                if (key is UidKey uidKey)
                {
                    UidGenerator.RemoveUid(uidKey.Uid);
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
                    errorMessage = $"Command with key <{key}> already added.";
                    return false;
                }
                if (_commandsToRemove.Contains(key))
                {
                    errorMessage = $"Command with key <{key}> is already scheduled for removal.";
                    return false;
                }
                if (_commandsToAdd.ContainsKey(key))
                {
                    errorMessage = $"Command with key <{key}> is already scheduled to add.";
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
            keyData = keyData ?? UidGenerator;
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
                var commandData = new SignalCommandModel(command, once);
                var commands = _dispatching ? _commandsToAdd : _commands;
                commands.Add(key, commandData);
                return command.Connect(out errorMessage);
            }
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
                    if (!command.Command.Release(out var errorMessage))
                    {

                    }
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

    public abstract class SignalBaseResultOut<T_Result, T_Out> : SignalBaseResult<T_Result>
    {
        #region Properties
        public virtual T_Out DefaultOut => default;
        #endregion
    }
}