using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public abstract class SignalResult<T_Result> : SignalBaseResult<T_Result>, ISignalResult<T_Result>
    {
        #region ISignalResult<T_Result> Members
        public IKey AddCommand(ActionResultDelegate<T_Result> callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionResultCommand<T_Result>(callback), keyData, once);
        }

        public IKey AddCommand(ICommandResult<T_Result> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result>
        {
            return AddCommandInternal<TCommand>(once);
        }

        public T_Result DispatchResult(T_Result startingResult)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                var currentResult = startingResult;
                foreach (var kvp in Commands)
                {
                    if (!IsScheduledForRemoval(kvp.Key) &&
                        kvp.Value.Command is ICommandResult<T_Result> command)
                    {
                        if (StopOnResult && ResultEquals(currentResult, TargetResult))
                        {
                            return currentResult;
                        }
                        currentResult = ResultAggregate(currentResult, command.Execute());
                    }
                }
                DispatchEnd();
                return currentResult;
            }
        }

        public T_Result DispatchResult()
        {
            return DispatchResult(DefaultResult);
        }
        #endregion
    }

    public abstract class SignalResult<T_Result, T_In> : SignalBaseResult<T_Result>, ISignalResult<T_Result, T_In>
    {
        #region ISignalResult<T_Result,T_In> Members
        public IKey AddCommand(ActionResultDelegate<T_Result, T_In> callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionResultCommand<T_Result, T_In>(callback), keyData, once);
        }

        public IKey AddCommand(ICommandResult<T_Result, T_In> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result, T_In>
        {
            return AddCommandInternal<TCommand>(once);
        }

        public T_Result DispatchResult(T_In @in, T_Result startingResult)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                var currentResult = startingResult;
                foreach (var kvp in Commands)
                {
                    if (!IsScheduledForRemoval(kvp.Key) &&
                        kvp.Value.Command is ICommandResult<T_Result, T_In> command)
                    {
                        if (StopOnResult && ResultEquals(currentResult, TargetResult))
                        {
                            return currentResult;
                        }
                        currentResult = ResultAggregate(currentResult, command.Execute(@in));
                    }
                }
                DispatchEnd();
                return currentResult;
            }
        }

        public T_Result DispatchResult(T_In @in)
        {
            return DispatchResult(@in, DefaultResult);
        }
        #endregion
    }

    public abstract class SignalResult<T_Result, T_In_1, T_In_2> : SignalBaseResult<T_Result>, ISignalResult<T_Result, T_In_1, T_In_2>
    {
        #region ISignalResult<T_Result,T_In_1,T_In_2> Members
        public IKey AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> callback, object keyData = null, bool once = false)
        {
            return AddCommandInternal(new ActionResultCommand<T_Result, T_In_1, T_In_2>(callback), keyData, once);
        }

        public IKey AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, object keyData = null, bool once = false)
        {
            return AddCommandInternal(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result, T_In_1, T_In_2>
        {
            return AddCommandInternal<TCommand>(once);
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult)
        {
            lock (_syncRoot)
            {
                DispatchBegin();
                var currentResult = startingResult;
                foreach (var kvp in Commands)
                {
                    if (!IsScheduledForRemoval(kvp.Key) &&
                        kvp.Value.Command is ICommandResult<T_Result, T_In_1, T_In_2> command)
                    {
                        if (StopOnResult && ResultEquals(currentResult, TargetResult))
                        {
                            return currentResult;
                        }
                        currentResult = ResultAggregate(currentResult, command.Execute(in1, in2));
                    }
                }
                DispatchEnd();
                return currentResult;
            }
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2)
        {
            return DispatchResult(in1, in2, DefaultResult);
        }
        #endregion
    }
}