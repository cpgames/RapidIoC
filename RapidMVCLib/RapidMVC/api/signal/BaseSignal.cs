using System;
using System.Collections.Generic;
using System.Linq;
using cpGames.core.RapidMVC.impl;

namespace cpGames.core.RapidMVC
{
    /// <summary>
    /// Signals are a way to execute commands.
    /// To use, connect your commands to a signal and the call signal.Dispatch(parameters) to execute connected commands.
    /// </summary>
    public abstract class BaseSignal
    {
        #region Fields
        private readonly Dictionary<IKey, IBaseCommand> _commands = new Dictionary<IKey, IBaseCommand>();
        private readonly Dictionary<IKey, IBaseCommand> _commandsToAdd = new Dictionary<IKey, IBaseCommand>();
        private readonly List<IKey> _commandsToRemove = new List<IKey>();
        private readonly UidGenerator _uidGenerator = new UidGenerator();
        private bool _dispatching;
        #endregion

        #region Properties
        public IEnumerable<IBaseCommand> Commands => _commands.Values;
        public int CommandCount => _commands.Count;
        #endregion

        #region Methods
        public void RemoveCommand(object keyData)
        {
            if (!Rapid.KeyFactoryCollection.Create(keyData, out var key, out var errorMessage) ||
                !RemoveCommandInternal(key, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public void RemoveCommand<TCommand>() where TCommand : IBaseCommand
        {
            RemoveCommand(typeof(TCommand));
        }

        protected bool RemoveCommandInternal(IKey key, out string errorMessage)
        {
            if (!_commands.TryGetValue(key, out var command))
            {
                errorMessage = string.Format("Command with key <{0}> not found.", key);
                return false;
            }
            if (_dispatching)
            {
                if (_commandsToRemove.Contains(key))
                {
                    errorMessage = string.Format("Command with key <{0}> is already scheduled for removal.", key);
                    return false;
                }
                _commandsToRemove.Add(key);
                errorMessage = string.Empty;
                return true;
            }
            command.Release();
            if (key is UidKey uidKey)
            {
                _uidGenerator.RemoveUid(uidKey.Uid);
            }
            _commands.Remove(key);
            errorMessage = string.Empty;
            return true;
        }

        public void ClearCommands()
        {
            while (_commands.Count > 0)
            {
                if (!RemoveCommandInternal(_commands.Keys.First(), out var errorMessage))
                {
                    throw new Exception(errorMessage);
                }
            }
        }

        private bool ValidateKey(IKey key, out string errorMessage)
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
            errorMessage = string.Empty;
            return true;
        }

        protected IKey AddCommandInternal<TCommand>(bool once) where TCommand : IBaseCommand
        {
            if (!Rapid.KeyFactoryCollection.Create(typeof(TCommand), out var key, out var errorMessage) ||
                !AddCommandInternal(key, once, out errorMessage))
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
            if (!ValidateKey(key, out errorMessage))
            {
                return false;
            }
            if (once)
            {
                _commandsToRemove.Add(key);
            }
            var commands = _dispatching ? _commandsToAdd : _commands;
            commands.Add(key, command);
            errorMessage = string.Empty;
            return true;
        }

        private bool AddCommandInternal(IKey key, bool once, out string errorMessage)
        {
            if (!ValidateKey(key, out errorMessage))
            {
                return false;
            }
            if (once)
            {
                _commandsToRemove.Add(key);
            }
            if (_commandsToAdd.ContainsKey(key))
            {
                errorMessage = string.Format("Command with key <{0}> is already scheduled to add.", key);
                return false;
            }
            var command = (IBaseCommand)Activator.CreateInstance(((TypeKey)key).Type);
            var commands = _dispatching ? _commandsToAdd : _commands;
            commands.Add(key, command);
            command.Connect();
            errorMessage = string.Empty;
            return true;
        }

        protected void DispatchBegin()
        {
            _dispatching = true;
        }

        protected void DispatchEnd()
        {
            foreach (var key in _commandsToRemove)
            {
                var command = _commands[key];
                command.Release();
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
        #endregion
    }
}