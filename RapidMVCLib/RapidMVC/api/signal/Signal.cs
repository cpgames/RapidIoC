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
        protected readonly List<IBaseCommand> _commands = new List<IBaseCommand>();
        protected readonly List<IBaseCommand> _onceCommands = new List<IBaseCommand>();
        #endregion

        #region Properties
        public int CommandCount => _commands.Count + _onceCommands.Count;
        #endregion

        #region Methods
        public bool RemoveCommand(IBaseCommand command)
        {
            return RemoveCommand(command, _commands) ||
                RemoveCommand(command, _onceCommands);
        }

        private bool RemoveCommand(IBaseCommand command, List<IBaseCommand> commands)
        {
            if (commands.Contains(command))
            {
                commands.Remove(command);
                command.Release();
                return true;
            }
            return false;
        }

        public void ClearCommands()
        {
            ClearCommandsInternal(_commands);
            ClearCommandsInternal(_onceCommands);
        }

        private void ClearCommandsInternal(List<IBaseCommand> commands)
        {
            while (commands.Count > 0)
            {
                RemoveCommand(commands[0]);
            }
        }

        protected virtual IBaseCommand AddCommandInternal(IBaseCommand command, bool once)
        {
            var commands = once ? _onceCommands : _commands;
            if (commands.Contains(command))
            {
                return null;
            }
            commands.Add(command);
            return command;
        }
        #endregion
    }

    /// <summary>
    /// Signal with no parameters
    /// </summary>
    public class Signal : BaseSignal
    {
        #region Methods
        public IBaseCommand AddCommand(Action callback, bool once = false)
        {
            return AddCommandInternal(new ActionCommand(callback), once);
        }

        public IBaseCommand AddCommand(ICommand command, bool once = false)
        {
            return AddCommandInternal(command, once);
        }

        public IBaseCommand AddCommand<TCommand>(bool once = false) where TCommand : ICommand
        {
            return AddCommandInternal(Activator.CreateInstance<TCommand>(), once);
        }

        public void Dispatch()
        {
            foreach (var command in _commands.OfType<ICommand>())
            {
                command.Execute();
            }
            foreach (var command in _onceCommands.OfType<ICommand>())
            {
                command.Execute();
                command.Release();
            }
            _onceCommands.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Signal with one parameter
    /// </summary>
    public class Signal<T> : BaseSignal
    {
        #region Methods
        public IBaseCommand AddCommand(Action<T> callback, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T>(callback), once);
        }

        public IBaseCommand AddCommand(ICommand<T> command, bool once = false)
        {
            return AddCommandInternal(command, once);
        }

        public IBaseCommand AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T>
        {
            return AddCommandInternal(Activator.CreateInstance<TCommand>(), once);
        }

        public void Dispatch(T type1)
        {
            foreach (var command in _commands.OfType<ICommand<T>>())
            {
                command.Execute(type1);
            }
            foreach (var command in _onceCommands.OfType<ICommand<T>>())
            {
                command.Execute(type1);
                command.Release();
            }
            _onceCommands.Clear();
        }
        #endregion
    }

    /// <summary>
    /// Signal with two parameters.
    /// If you want to have more, I recommend creating a model and using that as a parameter.
    /// </summary>
    public class Signal<T, U> : BaseSignal
    {
        #region Methods
        public IBaseCommand AddCommand(Action<T, U> callback, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T, U>(callback), once);
        }

        public IBaseCommand AddCommand(ICommand<T, U> command, bool once = false)
        {
            return AddCommandInternal(command, once);
        }

        public IBaseCommand AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T, U>
        {
            return AddCommandInternal(Activator.CreateInstance<TCommand>(), once);
        }

        public void Dispatch(T type1, U type2)
        {
            foreach (var command in _commands.OfType<ICommand<T, U>>())
            {
                command.Execute(type1, type2);
            }
            foreach (var command in _onceCommands.OfType<ICommand<T, U>>())
            {
                command.Execute(type1, type2);
                command.Release();
            }
            _onceCommands.Clear();
        }
        #endregion
    }
}