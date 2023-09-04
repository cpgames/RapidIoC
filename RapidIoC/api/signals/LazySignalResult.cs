using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public abstract class LazySignalResult<T_Result> : LazySignalBaseResult<T_Result>, ISignalResult<T_Result>
    {
        #region Fields
        private ISignalResult<T_Result>? _signal;
        #endregion

        #region Properties
        protected ISignalResult<T_Result> Signal => _signal ??= Factory();

        protected override ISignalBase SignalBase => Signal;

        public virtual bool IgnoreRecursiveDispatch { get; set; }
        #endregion

        #region ISignalResult<T_Result> Members
        public Outcome AddCommand(ICommandResult<T_Result> command, IKey key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommandResult<T_Result> command, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(
            ICommandResult<T_Result> command,
            out IKey key,
            object? keyData = null,
            bool once = false)
        {
            return Signal.AddCommand(
                command,
                out key,
                keyData,
                once);
        }

        public Outcome AddCommand(ActionResultDelegate<T_Result> action, IKey key, bool once = false)
        {
            return Signal.AddCommand(new ActionResultCommand<T_Result>(action), key, once);
        }

        public Outcome AddCommand(ActionResultDelegate<T_Result> action, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(new ActionResultCommand<T_Result>(action), keyData, once);
        }

        public Outcome AddCommand(
            ActionResultDelegate<T_Result> action,
            out IKey key,
            object? keyData = null,
            bool once = false)
        {
            return Signal.AddCommand(
                new ActionResultCommand<T_Result>(action),
                out key,
                keyData,
                once);
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
        private ISignalResult<T_Result, T_In>? _signal;
        #endregion

        #region Properties
        protected ISignalResult<T_Result, T_In> Signal => _signal ??= Factory();

        protected override ISignalBase SignalBase => Signal;
        public virtual bool IgnoreRecursiveDispatch { get; set; }
        #endregion

        #region ISignalResult<T_Result,T_In> Members
        public Outcome AddCommand(ICommandResult<T_Result, T_In> command, IKey key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommandResult<T_Result, T_In> command, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(
            ICommandResult<T_Result, T_In> command,
            out IKey key,
            object? keyData = null,
            bool once = false)
        {
            return Signal.AddCommand(
                command,
                out key,
                keyData,
                once);
        }

        public Outcome AddCommand(ActionResultDelegate<T_Result, T_In> action, IKey key, bool once = false)
        {
            return Signal.AddCommand(new ActionResultCommand<T_Result, T_In>(action), key, once);
        }

        public Outcome AddCommand(ActionResultDelegate<T_Result, T_In> action, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(new ActionResultCommand<T_Result, T_In>(action), keyData, once);
        }

        public Outcome AddCommand(
            ActionResultDelegate<T_Result, T_In> action,
            out IKey key,
            object? keyData = null,
            bool once = false)
        {
            return Signal.AddCommand(
                new ActionResultCommand<T_Result, T_In>(action),
                out key,
                keyData,
                once);
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
        private ISignalResult<T_Result, T_In_1, T_In_2>? _signal;
        #endregion

        #region Properties
        protected ISignalResult<T_Result, T_In_1, T_In_2> Signal => _signal ??= Factory();

        protected override ISignalBase SignalBase => Signal;
        public virtual bool IgnoreRecursiveDispatch { get; set; }
        #endregion

        #region ISignalResult<T_Result,T_In_1,T_In_2> Members
        public Outcome AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, IKey key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(
            ICommandResult<T_Result, T_In_1, T_In_2> command,
            out IKey key,
            object? keyData = null,
            bool once = false)
        {
            return Signal.AddCommand(
                command,
                out key,
                keyData,
                once);
        }

        public Outcome AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> action, IKey key, bool once = false)
        {
            return Signal.AddCommand(new ActionResultCommand<T_Result, T_In_1, T_In_2>(action), key, once);
        }

        public Outcome AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> action, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(new ActionResultCommand<T_Result, T_In_1, T_In_2>(action), keyData, once);
        }

        public Outcome AddCommand(
            ActionResultDelegate<T_Result, T_In_1, T_In_2> action,
            out IKey key,
            object? keyData = null,
            bool once = false)
        {
            return Signal.AddCommand(
                new ActionResultCommand<T_Result, T_In_1, T_In_2>(action),
                out key,
                keyData,
                once);
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