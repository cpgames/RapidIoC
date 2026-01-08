using System;
using System.Collections.Generic;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <inheritdoc cref="SignalBase" />
    /// <summary>
    /// Signal with no parameters
    /// </summary>
    public class Signal : SignalBase, ISignal
    {
        #region Fields
        private int _dispatchQueue;
        #endregion

        #region ISignal Members
        public Outcome AddCommand<TCommand>(bool once = false) where TCommand : ICommand
        {
            var createKeyResult = Rapid.KeyFactoryCollection.Create(typeof(TCommand), out var key);
            if (!createKeyResult)
            {
                return createKeyResult;
            }
            if (HasKey(key))
            {
                return Outcome.Fail($"Command with key <{key}> is already registered with the signal.");
            }
            var instantiator = new DefaultInstantiator<TCommand>();
            return
                instantiator.Create(out var command) &&
                AddCommand(command!, key, once);
        }

        public Outcome AddCommand<TCommand>(out IKey key, bool once = false) where TCommand : ICommand
        {
            var createKeyResult = Rapid.KeyFactoryCollection.Create(typeof(TCommand), out key);
            if (!createKeyResult)
            {
                return createKeyResult;
            }
            if (HasKey(key))
            {
                return Outcome.Fail($"Command with key <{key}> is already registered with the signal.");
            }
            var instantiator = new DefaultInstantiator<TCommand>();
            return
                instantiator.Create(out var command) &&
                AddCommand(command!, key, once);
        }

        public Outcome AddCommand(ICommand command, IKey key, bool once = false)
        {
            return AddCommandInternal(command, key, once);
        }

        public Outcome AddCommand(ICommand command, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public Outcome AddCommand(ICommand command, out IKey key, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(command, out key, keyData, once);
        }

        public Outcome AddCommand(Action action, IKey key, bool once = false)
        {
            return AddCommandInternal(new ActionCommand(action), key, once);
        }

        public Outcome AddCommand(Action action, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand(action), keyData, once);
        }

        public Outcome AddCommand(Action action, out IKey key, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand(action), out key, keyData, once);
        }

        /// <summary>
        /// Execute all commands added to the signal
        /// note: sometimes your commands may contain code to add more commands to the same signal instance,
        /// in this case they will be queued up and added after this dispatch, and not executed in this cycle
        /// note2: if one or more commands contains logic to remove commands from this signal, they will be removed
        /// after this dispatch, and will be executed in this cycle
        /// </summary>
        public void Dispatch()
        {
            lock (_syncRoot)
            {
                if (!DispatchBegin())
                {
                    _dispatchQueue++;
                }
                else
                {
                    foreach (var kvp in Commands)
                    {
                        if (!IsSuspended(kvp.Key) &&
                            !IsScheduledForRemoval(kvp.Key) &&
                            kvp.Value.Command is ICommand command)
                        {
                            command.Execute();
                        }
                    }
                    DispatchEnd();
                    if (_dispatchQueue > 0)
                    {
                        _dispatchQueue--;
                        Dispatch();
                    }
                }
            }
        }
        #endregion
    }

    /// <inheritdoc cref="SignalBase" />
    /// <summary>
    /// Signal with one parameter
    /// </summary>
    public class Signal<T_In> : SignalBase, ISignal<T_In>
    {
        #region Fields
        private readonly Queue<T_In> _dispatchQueue = new();
        #endregion

        #region ISignal<T_In> Members
        public Outcome AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T_In>
        {
            var createKeyResult = Rapid.KeyFactoryCollection.Create(typeof(TCommand), out var key);
            if (!createKeyResult)
            {
                return createKeyResult;
            }
            if (HasKey(key))
            {
                return Outcome.Fail($"Command with key <{key}> is already registered with the signal.");
            }
            var instantiator = new DefaultInstantiator<TCommand>();
            return
                instantiator.Create(out var command) &&
                AddCommand(command!, key, once);
        }

        public Outcome AddCommand<TCommand>(out IKey key, bool once = false) where TCommand : ICommand<T_In>
        {
            var createKeyResult = Rapid.KeyFactoryCollection.Create(typeof(TCommand), out key);
            if (!createKeyResult)
            {
                return createKeyResult;
            }
            if (HasKey(key))
            {
                return Outcome.Fail($"Command with key <{key}> is already registered with the signal.");
            }
            var instantiator = new DefaultInstantiator<TCommand>();
            return
                instantiator.Create(out var command) &&
                AddCommand(command!, key, once);
        }

        public Outcome AddCommand(ICommand<T_In> command, IKey key, bool once = false)
        {
            return AddCommandInternal(command, key, once);
        }

        public Outcome AddCommand(ICommand<T_In> command, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public Outcome AddCommand(ICommand<T_In> command, out IKey key, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(command, out key, keyData, once);
        }

        public Outcome AddCommand(Action<T_In> action, IKey key, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T_In>(action), key, once);
        }

        public Outcome AddCommand(Action<T_In> action, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T_In>(action), keyData, once);
        }

        public Outcome AddCommand(Action<T_In> action, out IKey key, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T_In>(action), out key, keyData, once);
        }

        /// <summary>
        /// Execute all commands added to the signal
        /// note: sometimes your commands may contain code to add more commands to the same signal instance,
        /// in this case they will be queued up and added after this dispatch, and not executed in this cycle
        /// note2: if one or more commands contains logic to remove commands from this signal, they will be removed
        /// after this dispatch, and WILL be executed in this cycle
        /// </summary>
        /// <param name="in">First parameter data to pass to commands</param>
        public void Dispatch(T_In @in)
        {
            lock (_syncRoot)
            {
                if (!DispatchBegin())
                {
                    _dispatchQueue.Enqueue(@in);
                }
                else
                {
                    foreach (var kvp in Commands)
                    {
                        if (!IsSuspended(kvp.Key) && 
                            !IsScheduledForRemoval(kvp.Key) &&
                            kvp.Value.Command is ICommand<T_In> command)
                        {
                            command.Execute(@in);
                        }
                    }
                    DispatchEnd();
                    if (_dispatchQueue.Count > 0)
                    {
                        Dispatch(_dispatchQueue.Dequeue());
                    }
                }
            }
        }
        #endregion
    }

    /// <inheritdoc cref="SignalBase" />
    /// <summary>
    /// Signal with two parameters.
    /// If you want to have more, I recommend creating a model and using that as a single-parameter signal.
    /// </summary>
    public class Signal<T_In_1, T_In_2> : SignalBase, ISignal<T_In_1, T_In_2>
    {
        #region Fields
        private readonly Queue<KeyValuePair<T_In_1, T_In_2>> _dispatchQueue = new();
        #endregion

        #region ISignal<T_In_1,T_In_2> Members
        public Outcome AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T_In_1, T_In_2>
        {
            var createKeyResult = Rapid.KeyFactoryCollection.Create(typeof(TCommand), out var key);
            if (!createKeyResult)
            {
                return createKeyResult;
            }
            if (HasKey(key))
            {
                return Outcome.Fail($"Command with key <{key}> is already registered with the signal.");
            }
            var instantiator = new DefaultInstantiator<TCommand>();
            return
                instantiator.Create(out var command) &&
                AddCommand(command!, key, once);
        }

        public Outcome AddCommand<TCommand>(out IKey key, bool once = false) where TCommand : ICommand<T_In_1, T_In_2>
        {
            var createKeyResult = Rapid.KeyFactoryCollection.Create(typeof(TCommand), out key);
            if (!createKeyResult)
            {
                return createKeyResult;
            }
            if (HasKey(key))
            {
                return Outcome.Fail($"Command with key <{key}> is already registered with the signal.");
            }
            var instantiator = new DefaultInstantiator<TCommand>();
            return
                instantiator.Create(out var command) &&
                AddCommand(command!, key, once);
        }

        public Outcome AddCommand(ICommand<T_In_1, T_In_2> command, IKey key, bool once = false)
        {
            return AddCommandInternal(command, key, once);
        }

        public Outcome AddCommand(ICommand<T_In_1, T_In_2> command, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public Outcome AddCommand(ICommand<T_In_1, T_In_2> command, out IKey key, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(command, out key, keyData, once);
        }

        public Outcome AddCommand(Action<T_In_1, T_In_2> action, IKey key, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T_In_1, T_In_2>(action), key, once);
        }

        public Outcome AddCommand(Action<T_In_1, T_In_2> action, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T_In_1, T_In_2>(action), keyData, once);
        }

        public Outcome AddCommand(Action<T_In_1, T_In_2> action, out IKey key, object? keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionCommand<T_In_1, T_In_2>(action), out key, keyData, once);
        }

        /// <summary>
        /// Execute all commands added to the signal
        /// note: sometimes your commands may contain code to add more commands to the same signal instance,
        /// in this case they will be queued up and added after this dispatch, and not executed in this cycle
        /// note2: if one or more commands contains logic to remove commands from this signal, they will be removed
        /// after this dispatch, and WILL be executed in this cycle
        /// </summary>
        /// <param name="in1">First parameter data to pass to commands</param>
        /// <param name="in2">Second parameter data to pass to commands</param>
        public void Dispatch(T_In_1 in1, T_In_2 in2)
        {
            lock (_syncRoot)
            {
                if (!DispatchBegin())
                {
                    _dispatchQueue.Enqueue(new KeyValuePair<T_In_1, T_In_2>(in1, in2));
                }
                else
                {
                    foreach (var kvp in Commands)
                    {
                        if (!IsSuspended(kvp.Key) && 
                            !IsScheduledForRemoval(kvp.Key) &&
                            kvp.Value.Command is ICommand<T_In_1, T_In_2> command)
                        {
                            command.Execute(in1, in2);
                        }
                    }
                    DispatchEnd();
                    if (_dispatchQueue.Count > 0)
                    {
                        var kvp = _dispatchQueue.Dequeue();
                        Dispatch(kvp.Key, kvp.Value);
                    }
                }
            }
        }
        #endregion
    }
}