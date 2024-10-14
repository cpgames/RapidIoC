using System;
using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    /// <summary>
    ///     Signals are a way to execute commands.
    ///     To use, connect your commands to a signal and the call signal.Dispatch(parameters) to execute connected commands.
    /// </summary>
    public abstract class SignalBase : ISignalBase
    {
        #region Fields
        private readonly Dictionary<IKey, SignalCommandModel> _commands = new();
        private readonly Dictionary<IKey, SignalCommandModel> _commandsToAdd = new();
        private readonly HashSet<IKey> _commandsToRemove = new();
        private readonly HashSet<IKey> _suspendedCommands = new();
        protected internal readonly object _syncRoot = new();
        #endregion

        #region ISignalBase Members
        public IEnumerable<KeyValuePair<IKey, SignalCommandModel>> Commands => _commands;
        public int CommandCount => _commands.Count;
        public bool IsDispatching { get; private set; }

        public bool IsScheduledForRemoval(IKey key)
        {
            return _commandsToRemove.Contains(key);
        }

        public bool HasKey(object keyData)
        {
            lock (_syncRoot)
            {
                return
                    Rapid.KeyFactoryCollection.Create(keyData, out var key) &&
                    HasKey(key);
            }
        }

        public Outcome RemoveCommand(object? keyData)
        {
            return
                Rapid.KeyFactoryCollection.Create(keyData, out var key) &&
                RemoveCommandInternal(key);
        }

        public Outcome RemoveCommand<TCommand>() where TCommand : IBaseCommand
        {
            return RemoveCommand(typeof(TCommand));
        }

        public Outcome ClearCommands()
        {
            lock (_syncRoot)
            {
                while (_commands.Count > 0)
                {
                    var removeCommandResult = RemoveCommandInternal(_commands.Keys.First());
                    if (!removeCommandResult)
                    {
                        return removeCommandResult;
                    }
                }
                return Outcome.Success();
            }
        }

        public Outcome SuspendCommand(object keyData)
        {
            lock (_syncRoot)
            {
                var createKeyOutcome = Rapid.KeyFactoryCollection.Create(keyData, out var key);
                if (!createKeyOutcome)
                {
                    return createKeyOutcome;
                }
                if (!_commands.ContainsKey(key))
                {
                    return Outcome.Fail($"Command with key <{key}> not found.");
                }
                if (_commandsToRemove.Contains(key))
                {
                    return Outcome.Fail($"Command with key <{key}> is scheduled for removal.");
                }
                if (!_suspendedCommands.Add(key))
                {
                    return Outcome.Fail($"Command with key <{key}> is already suspended.");
                }
                return Outcome.Success();
            }
        }

        public Outcome ResumeCommand(object keyData)
        {
            lock (_syncRoot)
            {
                var createKeyOutcome = Rapid.KeyFactoryCollection.Create(keyData, out var key);
                if (!createKeyOutcome)
                {
                    return createKeyOutcome;
                }
                if (!_suspendedCommands.Remove(key))
                {
                    return Outcome.Fail($"Command with key <{key}> is not suspended.");
                }
                return Outcome.Success();
            }
        }

        public bool IsSuspended(object keyData)
        {
            return
                Rapid.KeyFactoryCollection.Create(keyData, out var key) &&
                _suspendedCommands.Contains(key);
        }
        #endregion

        #region Methods
        public bool HasKey(IKey key)
        {
            lock (_syncRoot)
            {
                return _commands.ContainsKey(key) || _commandsToAdd.ContainsKey(key);
            }
        }

        protected Outcome RemoveCommandInternal(IKey key)
        {
            lock (_syncRoot)
            {
                if (!_commands.TryGetValue(key, out var commandData))
                {
                    return Outcome.Fail($"Command with key <{key}> not found.");
                }
                _suspendedCommands.Remove(key);
                if (IsDispatching)
                {
                    if (!_commandsToRemove.Add(key))
                    {
                        return Outcome.Fail($"Command with key <{key}> is already scheduled for removal.");
                    }
                    return Outcome.Success();
                }
                var releaseCommandResult = commandData.Command.Release();
                if (!releaseCommandResult)
                {
                    return releaseCommandResult;
                }
                if (key is IdKey idKey)
                {
                    var removeIdResult = Rapid._idContainer.RemoveKey(idKey);
                    if (!removeIdResult)
                    {
                        return removeIdResult;
                    }
                }
                _commands.Remove(key);
            }
            return Outcome.Success();
        }

        private Outcome ValidateKey(IKey key)
        {
            lock (_syncRoot)
            {
                if (_commands.ContainsKey(key))
                {
                    return Outcome.Fail($"Command with key <{key}> already added.");
                }
                if (_commandsToRemove.Contains(key))
                {
                    return Outcome.Fail($"Command with key <{key}> is already scheduled for removal.");
                }
                if (_commandsToAdd.ContainsKey(key))
                {
                    return Outcome.Fail($"Command with key <{key}> is already scheduled to add.");
                }
            }
            return Outcome.Success();
        }

        protected Outcome AddCommandInternal(IBaseCommand command, IKey key, bool once)
        {
            lock (_syncRoot)
            {
                var result = ValidateKey(key);
                if (!result)
                {
                    return result;
                }
                var commandData = new SignalCommandModel(command, once);
                var commands = IsDispatching ? _commandsToAdd : _commands;
                commands.Add(key, commandData);
                return command.Connect();
            }
        }

        protected Outcome AddCommandInternal(IBaseCommand command, object? keyData, bool once)
        {
            return AddCommandInternal(
                command,
                out _,
                keyData,
                once);
        }

        protected Outcome AddCommandInternal(
            IBaseCommand command,
            out IKey key,
            object? keyData,
            bool once)
        {
            if (keyData == null)
            {
                var createKeyOutcome = Rapid._idContainer.CreateKey(out key);
                if (!createKeyOutcome)
                {
                    return createKeyOutcome;
                }
            }
            else
            {
                var createKeyOutcome = Rapid.KeyFactoryCollection.Create(keyData, out key);
                if (!createKeyOutcome)
                {
                    return createKeyOutcome;
                }
            }
            return AddCommandInternal(command, key, once);
        }

        protected Outcome DispatchBegin()
        {
            if (IsDispatching)
            {
                return Outcome.Fail($"{GetType().Name} is already dispatching, recursive execution for this Signal type is not supported.");
            }
            IsDispatching = true;
            return Outcome.Success();
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
                    var releaseCommandResult = command.Command.Release();
                    if (!releaseCommandResult)
                    {
                        throw new Exception(releaseCommandResult.ErrorMessage);
                    }
                    _commands.Remove(key);
                }
                _commandsToRemove.Clear();

                foreach (var kvp in _commandsToAdd)
                {
                    _commands.Add(kvp.Key, kvp.Value);
                }
                _commandsToAdd.Clear();
                IsDispatching = false;
            }
        }
        #endregion
    }

    public abstract class SignalBaseResult<T_Result> : SignalBase
    {
        #region Properties
        public abstract T_Result DefaultResult { get; }
        public abstract T_Result TargetResult { get; }
        public virtual bool StopOnResult => false;
        public virtual bool IgnoreRecursiveDispatch { get; set; }
        #endregion

        #region Methods
        public abstract bool ResultEquals(T_Result a, T_Result b);

        public abstract T_Result ResultAggregate(T_Result a, T_Result b);
        #endregion
    }

    public abstract class SignalBaseResultOut<T_Result, T_Out> : SignalBaseResult<T_Result>
    {
        #region Properties
        public abstract T_Out DefaultOut { get; }
        #endregion
    }
}