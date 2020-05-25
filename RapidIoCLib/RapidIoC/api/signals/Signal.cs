using System;
using System.Linq;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <inheritdoc cref="BaseSignal" />
    /// <summary>
    /// Signal with no parameters
    /// </summary>
    public class Signal : BaseSignal
    {
        #region Methods
        /// <summary>
        /// Add command with an action callback
        /// </summary>
        /// <param name="callback">Action to execute</param>
        /// <param name="keyData">Unique key data to bind command to, passing null will generate random Uid key</param>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand(Action callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand(callback), keyData, once);
        }

        /// <summary>
        /// Add command with a command object
        /// </summary>
        /// <param name="command">Command object</param>
        /// <param name="keyData">Unique key data to bind command to, passing null will generate random Uid key</param>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand(ICommand command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        /// <summary>
        /// Create new command object of type TCommand and add it, it's keydata will be its type
        /// note: only one type of this command object can be added
        /// </summary>
        /// <typeparam name="TCommand">Type of command object to instantiate</typeparam>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommand
        {
            return AddCommandInternal<TCommand>(once);
        }

        /// <summary>
        /// Execute all commands added to the signal
        /// note: sometimes your commands may contain code to add more commands to the same signal instance,
        /// in this case they will be queued up and added after this dispatch, and not executed in this cycle
        /// note2: if one or more commands contains logic to remove commands from this signal, they will be removed
        /// after this dispatch, and WILL be executed in this cycle
        /// </summary>
        public void Dispatch()
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                foreach (var command in Commands.OfType<ICommand>())
                {
                    command.Execute();
                }
                DispatchEnd();
            }
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
        /// <summary>
        /// Add command with an action callback
        /// </summary>
        /// <param name="callback">Action to execute</param>
        /// <param name="keyData">Unique key data to bind command to, passing null will generate random Uid key</param>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand(Action<T> callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T>(callback), keyData, once);
        }

        /// <summary>
        /// Add command with a command object
        /// </summary>
        /// <param name="command">Command object</param>
        /// <param name="keyData">Unique key data to bind command to, passing null will generate random Uid key</param>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand(ICommand<T> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        /// <summary>
        /// Create new command object of type TCommand and add it, it's keydata will be its type
        /// note: only one type of this command object can be added
        /// </summary>
        /// <typeparam name="TCommand">Type of command object to instantiate</typeparam>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T>
        {
            return AddCommandInternal<TCommand>(once);
        }

        /// <summary>
        /// Execute all commands added to the signal
        /// note: sometimes your commands may contain code to add more commands to the same signal instance,
        /// in this case they will be queued up and added after this dispatch, and not executed in this cycle
        /// note2: if one or more commands contains logic to remove commands from this signal, they will be removed
        /// after this dispatch, and WILL be executed in this cycle
        /// </summary>
        /// <param name="type1">First parameter data to pass to commands</param>
        public void Dispatch(T type1)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                foreach (var command in Commands.OfType<ICommand<T>>())
                {
                    command.Execute(type1);
                }
                DispatchEnd();
            }
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
        /// <summary>
        /// Add command with an action callback
        /// </summary>
        /// <param name="callback">Action to execute</param>
        /// <param name="keyData">Unique key data to bind command to, passing null will generate random Uid key</param>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand(Action<T, U> callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T, U>(callback), keyData, once);
        }

        /// <summary>
        /// Add command with a command object
        /// </summary>
        /// <param name="command">Command object</param>
        /// <param name="keyData">Unique key data to bind command to, passing null will generate random Uid key</param>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand(ICommand<T, U> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        /// <summary>
        /// Create new command object of type TCommand and add it, it's keydata will be its type
        /// note: only one type of this command object can be added
        /// </summary>
        /// <typeparam name="TCommand">Type of command object to instantiate</typeparam>
        /// <param name="once">If true, command will be removed after first execution</param>
        /// <returns>Key instance the command is binded to, which can then be used to explicitly remove it from the signal</returns>
        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T, U>
        {
            return AddCommandInternal<TCommand>(once);
        }

        /// <summary>
        /// Execute all commands added to the signal
        /// note: sometimes your commands may contain code to add more commands to the same signal instance,
        /// in this case they will be queued up and added after this dispatch, and not executed in this cycle
        /// note2: if one or more commands contains logic to remove commands from this signal, they will be removed
        /// after this dispatch, and WILL be executed in this cycle
        /// </summary>
        /// <param name="type1">First parameter data to pass to commands</param>
        /// <param name="type2">Second parameter data to pass to commands</param>
        public void Dispatch(T type1, U type2)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                foreach (var command in Commands.OfType<ICommand<T, U>>())
                {
                    command.Execute(type1, type2);
                }
                DispatchEnd();
            }
        }
        #endregion
    }
}