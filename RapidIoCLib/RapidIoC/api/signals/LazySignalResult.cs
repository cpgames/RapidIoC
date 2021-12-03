using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public abstract class LazySignalResult<T_Result> : LazySignalBaseResult<T_Result>, ISignalResult<T_Result>
    {
        #region Fields
        private ISignalResult<T_Result> _signal;
        #endregion

        #region Properties
        protected ISignalResult<T_Result> Signal => _signal ?? (_signal = Factory());

        protected override ISignalBase SignalBase => _signal;
        #endregion

        #region ISignalResult<T_Result> Members
        public IKey AddCommand(ActionResultDelegate<T_Result> callback, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(callback, keyData, once);
        }

        public IKey AddCommand(ICommandResult<T_Result> command, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result>
        {
            return Signal.AddCommand<TCommand>(once);
        }

        public T_Result DispatchResult(T_Result startingResult)
        {
            return _signal == null ? startingResult : _signal.DispatchResult(startingResult);
        }

        public T_Result DispatchResult()
        {
            return _signal == null ? DefaultResult : _signal.DispatchResult();
        }
        #endregion

        #region Methods
        protected abstract ISignalResult<T_Result> Factory();
        #endregion
    }

    public abstract class LazySignalResult<T_Result, T_In> : LazySignalBaseResult<T_Result>, ISignalResult<T_Result, T_In>
    {
        #region Fields
        private ISignalResult<T_Result, T_In> _signal;
        #endregion

        #region Properties
        protected ISignalResult<T_Result, T_In> Signal => _signal ?? (_signal = Factory());

        protected override ISignalBase SignalBase => _signal;
        #endregion

        #region ISignalResult<T_Result,T_In> Members
        public IKey AddCommand(ActionResultDelegate<T_Result, T_In> callback, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(callback, keyData, once);
        }

        public IKey AddCommand(ICommandResult<T_Result, T_In> command, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result, T_In>
        {
            return Signal.AddCommand<TCommand>(once);
        }

        public T_Result DispatchResult(T_In @in, T_Result startingResult)
        {
            return _signal == null ? startingResult : _signal.DispatchResult(@in, startingResult);
        }

        public T_Result DispatchResult(T_In @in)
        {
            return _signal == null ? DefaultResult : _signal.DispatchResult(@in);
        }
        #endregion

        #region Methods
        protected abstract ISignalResult<T_Result, T_In> Factory();
        #endregion
    }

    public abstract class LazySignalResult<T_Result, T_In_1, T_In_2> : LazySignalBaseResult<T_Result>, ISignalResult<T_Result, T_In_1, T_In_2>
    {
        #region Fields
        private ISignalResult<T_Result, T_In_1, T_In_2> _signal;
        #endregion

        #region Properties
        protected ISignalResult<T_Result, T_In_1, T_In_2> Signal => _signal ?? (_signal = Factory());

        protected override ISignalBase SignalBase => _signal;
        #endregion

        #region ISignalResult<T_Result,T_In_1,T_In_2> Members
        public IKey AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> callback, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(callback, keyData, once);
        }

        public IKey AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result, T_In_1, T_In_2>
        {
            return Signal.AddCommand<TCommand>(once);
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult)
        {
            return _signal == null ? startingResult : _signal.DispatchResult(in1, in2, startingResult);
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2)
        {
            return _signal == null ? DefaultResult : _signal.DispatchResult(in1, in2);
        }
        #endregion

        #region Methods
        protected abstract ISignalResult<T_Result, T_In_1, T_In_2> Factory();
        #endregion
    }
}