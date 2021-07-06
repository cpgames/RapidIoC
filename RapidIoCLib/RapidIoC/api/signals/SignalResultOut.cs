using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public abstract class SignalResultOut<T_Result, T_Out> : SignalBaseResult<T_Result>
    {
        #region Methods
        public IKey AddCommand(ActionResultOutCommand<T_Result, T_Out>.ActionResultDelegate callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionResultOutCommand<T_Result, T_Out>(callback), keyData, once);
        }

        public IKey AddCommand(ICommandResultOut<T_Result, T_Out> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResultOut<T_Result, T_Out>
        {
            return AddCommandInternal<TCommand>(once);
        }

        public T_Result DispatchResult(T_Result startingResult, out T_Out @out)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                var currentResult = startingResult;
                @out = default;
                foreach (var kvp in Commands)
                {
                    if (!IsScheduledForRemoval(kvp.Key) &&
                        kvp.Value.Command is ICommandResultOut<T_Result, T_Out> command)
                    {
                        if (StopOnResult && ResultEquals(currentResult, TargetResult))
                        {
                            return currentResult;
                        }
                        currentResult = ResultAggregate(currentResult, command.Execute(out @out));
                    }
                }
                DispatchEnd();
                return currentResult;
            }
        }

        public T_Result DispatchResult(out T_Out @out)
        {
            return DispatchResult(DefaultResult, out @out);
        }
        #endregion
    }

    public abstract class SignalResultOut<T_Result, T_In, T_Out> : SignalBaseResult<T_Result>
    {
        #region Methods
        public IKey AddCommand(ActionResultOutCommand<T_Result, T_In, T_Out>.ActionResultDelegate callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionResultOutCommand<T_Result, T_In, T_Out>(callback), keyData, once);
        }

        public IKey AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResultOut<T_Result, T_In, T_Out>
        {
            return AddCommandInternal<TCommand>(once);
        }

        public T_Result DispatchResult(T_In @in, T_Result startingResult, out T_Out @out)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                var currentResult = startingResult;
                @out = default;
                foreach (var kvp in Commands)
                {
                    if (!IsScheduledForRemoval(kvp.Key) &&
                        kvp.Value.Command is ICommandResultOut<T_Result, T_In, T_Out> command)
                    {
                        if (StopOnResult && ResultEquals(currentResult, TargetResult))
                        {
                            return currentResult;
                        }
                        currentResult = ResultAggregate(currentResult, command.Execute(@in, out @out));
                    }
                }
                DispatchEnd();
                return currentResult;
            }
        }

        public T_Result DispatchResult(T_In @in, out T_Out @out)
        {
            return DispatchResult(@in, DefaultResult, out @out);
        }
        #endregion
    }
    public abstract class SignalResultOut<T_Result, T_In_1, T_In_2, T_Out> : SignalBaseResult<T_Result>
    {
        #region Methods
        public IKey AddCommand(ActionResultOutCommand<T_Result, T_In_1, T_In_2, T_Out>.ActionResultDelegate callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionResultOutCommand<T_Result, T_In_1, T_In_2, T_Out>(callback), keyData, once);
        }

        public IKey AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out>
        {
            return AddCommandInternal<TCommand>(once);
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult, out T_Out @out)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                var currentResult = startingResult;
                @out = default;
                foreach (var kvp in Commands)
                {
                    if (!IsScheduledForRemoval(kvp.Key) &&
                        kvp.Value.Command is ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command)
                    {
                        if (StopOnResult && ResultEquals(currentResult, TargetResult))
                        {
                            return currentResult;
                        }
                        currentResult = ResultAggregate(currentResult, command.Execute(in1, in2, out @out));
                    }
                }
                DispatchEnd();
                return currentResult;
            }
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2, out T_Out @out)
        {
            return DispatchResult(in1, in2, DefaultResult, out @out);
        }
        #endregion
    }
}