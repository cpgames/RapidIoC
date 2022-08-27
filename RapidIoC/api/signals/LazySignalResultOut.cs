using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public abstract class LazySignalResultOut<T_Result, T_Out> : LazySignalBaseResultOut<T_Result, T_Out>, ISignalResultOut<T_Result, T_Out>
    {
        #region Fields
        private ISignalResultOut<T_Result, T_Out>? _signal;
        #endregion

        #region Properties
        protected ISignalResultOut<T_Result, T_Out> Signal => _signal ??= Factory();
        protected override ISignalBase SignalBase => Signal;
        #endregion

        #region ISignalResultOut<T_Result,T_Out> Members
        public Outcome AddCommand(ICommandResultOut<T_Result, T_Out> command, IKey key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommandResultOut<T_Result, T_Out> command, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(ICommandResultOut<T_Result, T_Out> command, out IKey key, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, out key, keyData, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_Out> action, IKey key, bool once = false)
        {
            return Signal.AddCommand(action, key, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_Out> action, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, keyData, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_Out> action, out IKey key, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, out key, keyData, once);
        }

        public T_Result DispatchResult(T_Result startingResult, out T_Out @out)
        {
            if (_signal == null)
            {
                @out = DefaultOut;
                return startingResult;
            }
            return _signal.DispatchResult(startingResult, out @out);
        }

        public T_Result DispatchResult(out T_Out @out)
        {
            if (_signal == null)
            {
                @out = DefaultOut;
                return DefaultResult;
            }
            return _signal.DispatchResult(out @out);
        }
        #endregion

        #region Methods
        protected abstract ISignalResultOut<T_Result, T_Out> Factory();
        #endregion
    }

    public abstract class LazySignalResultOut<T_Result, T_In, T_Out> : LazySignalBaseResultOut<T_Result, T_Out>, ISignalResultOut<T_Result, T_In, T_Out>
    {
        #region Fields
        private ISignalResultOut<T_Result, T_In, T_Out>? _signal;
        #endregion

        #region Properties
        protected ISignalResultOut<T_Result, T_In, T_Out> Signal => _signal ??= Factory();
        protected override ISignalBase SignalBase => Signal;
        #endregion

        #region ISignalResultOut<T_Result,T_In,T_Out> Members
        public Outcome AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, IKey key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, out IKey key, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, out key, keyData, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In, T_Out> action, IKey key, bool once = false)
        {
            return Signal.AddCommand(action, key, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In, T_Out> action, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, keyData, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In, T_Out> action, out IKey key, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, out key, keyData, once);
        }

        public T_Result DispatchResult(T_In @in, T_Result startingResult, out T_Out @out)
        {
            if (_signal == null)
            {
                @out = DefaultOut;
                return startingResult;
            }
            return _signal.DispatchResult(@in, startingResult, out @out);
        }

        public T_Result DispatchResult(T_In @in, out T_Out @out)
        {
            if (_signal == null)
            {
                @out = DefaultOut;
                return DefaultResult;
            }
            return _signal.DispatchResult(@in, out @out);
        }
        #endregion

        #region Methods
        protected abstract ISignalResultOut<T_Result, T_In, T_Out> Factory();
        #endregion
    }

    public abstract class LazySignalResultOut<T_Result, T_In_1, T_In_2, T_Out> : LazySignalBaseResultOut<T_Result, T_Out>, ISignalResultOut<T_Result, T_In_1, T_In_2, T_Out>
    {
        #region Fields
        private ISignalResultOut<T_Result, T_In_1, T_In_2, T_Out>? _signal;
        #endregion

        #region Properties
        protected ISignalResultOut<T_Result, T_In_1, T_In_2, T_Out> Signal => _signal ??= Factory();
        protected override ISignalBase SignalBase => Signal;
        #endregion

        #region ISignalResultOut<T_Result,T_In_1,T_In_2,T_Out> Members
        public Outcome AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, IKey key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, out IKey key, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, out key, keyData, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In_1, T_In_2, T_Out> action, IKey key, bool once = false)
        {
            return Signal.AddCommand(action, key, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In_1, T_In_2, T_Out> action, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, keyData, once);
        }

        public Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In_1, T_In_2, T_Out> action, out IKey key, object? keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, out key, keyData, once);
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult, out T_Out @out)
        {
            if (_signal == null)
            {
                @out = DefaultOut;
                return startingResult;
            }
            return _signal.DispatchResult(in1, in2, startingResult, out @out);
        }

        public T_Result DispatchResult(T_In_1 in1, T_In_2 in2, out T_Out @out)
        {
            if (_signal == null)
            {
                @out = DefaultOut;
                return DefaultResult;
            }
            return _signal.DispatchResult(in1, in2, out @out);
        }
        #endregion

        #region Methods
        protected abstract ISignalResultOut<T_Result, T_In_1, T_In_2, T_Out> Factory();
        #endregion
    }
}