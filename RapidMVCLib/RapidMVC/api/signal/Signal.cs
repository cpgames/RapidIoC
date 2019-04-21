using System;
using System.Linq;
using cpGames.core.RapidMVC.impl;

namespace cpGames.core.RapidMVC
{
    /// <inheritdoc cref="BaseSignal" />
    /// <summary>
    /// Signal with no parameters
    /// </summary>
    public class Signal : BaseSignal
    {
        #region Methods
        public void AddCommand(Action callback, object keyData = null, bool once = false)
        {
            AddCommandInternal(new ActionCommand(callback), keyData, once);
        }

        public void AddCommand(ICommand command, object keyData = null, bool once = false)
        {
            AddCommandInternal(command, keyData, once);
        }

        public void AddCommand<TCommand>(bool once = false) where TCommand : ICommand
        {
            AddCommandInternal<TCommand>(once);
        }

        public void Dispatch()
        {
            DispatchBegin();
            foreach (var command in Commands.OfType<ICommand>())
            {
                command.Execute();
            }
            DispatchEnd();
        }
        #endregion
    }

    /// <inheritdoc cref="BaseSignal" />
    /// <summary>
    /// Signal with one parameter
    /// </summary>
    public class Signal<T> : BaseSignal
    {
        #region Methods
        public void AddCommand(Action<T> callback, object keyData = null, bool once = false)
        {
            AddCommandInternal(new ActionCommand<T>(callback), keyData, once);
        }

        public void AddCommand(ICommand<T> command, object keyData = null, bool once = false)
        {
            AddCommandInternal(command, keyData, once);
        }

        public void AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T>
        {
            AddCommandInternal<TCommand>(once);
        }

        public void Dispatch(T type1)
        {
            DispatchBegin();
            foreach (var command in Commands.OfType<ICommand<T>>())
            {
                command.Execute(type1);
            }
            DispatchEnd();
        }
        #endregion
    }

    /// <inheritdoc cref="BaseSignal" />
    /// <summary>
    /// Signal with two parameters.
    /// If you want to have more, I recommend creating a model and using that as a single-parameter signal.
    /// </summary>
    public class Signal<T, U> : BaseSignal
    {
        #region Methods
        public void AddCommand(Action<T, U> callback, object keyData = null, bool once = false)
        {
            AddCommandInternal(new ActionCommand<T, U>(callback), keyData, once);
        }

        public void AddCommand(ICommand<T, U> command, object keyData = null, bool once = false)
        {
            AddCommandInternal(command, keyData, once);
        }

        public void AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T, U>
        {
            AddCommandInternal<TCommand>(once);
        }

        public void Dispatch(T type1, U type2)
        {
            DispatchBegin();
            foreach (var command in Commands.OfType<ICommand<T, U>>())
            {
                command.Execute(type1, type2);
            }
            DispatchEnd();
        }
        #endregion
    }
}