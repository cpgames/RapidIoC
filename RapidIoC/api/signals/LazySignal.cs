// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public class LazySignal : LazySignalBase, ISignal
    {
        #region Fields
        private ISignal _signal;
        #endregion

        #region Properties
        protected ISignal Signal => _signal ?? (_signal = Factory());
        protected override ISignalBase SignalBase => _signal;
        #endregion

        #region ISignal Members
        public Outcome AddCommand(ICommand command, IKey? key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommand command, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(ICommand command, out IKey? key, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, out key, keyData, once);
        }

        public Outcome AddCommand(Action action, IKey? key, bool once = false)
        {
            return Signal.AddCommand(action, key, once);
        }

        public Outcome AddCommand(Action action, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, keyData, once);
        }

        public Outcome AddCommand(Action action, out IKey? key, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, out key, keyData, once);
        }

        public void Dispatch()
        {
            _signal?.Dispatch();
        }
        #endregion

        #region Methods
        protected virtual ISignal Factory()
        {
            return new Signal();
        }
        #endregion
    }

    public class LazySignal<T_In> : LazySignalBase, ISignal<T_In>
    {
        #region Fields
        private ISignal<T_In> _signal;
        #endregion

        #region Properties
        protected ISignal<T_In> Signal => _signal ?? (_signal = Factory());
        protected override ISignalBase SignalBase => _signal;
        #endregion

        #region ISignal<T_In> Members
        public Outcome AddCommand(ICommand<T_In> command, IKey? key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommand<T_In> command, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(ICommand<T_In> command, out IKey? key, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, out key, keyData, once);
        }

        public Outcome AddCommand(Action<T_In> action, IKey? key, bool once = false)
        {
            return Signal.AddCommand(action, key, once);
        }

        public Outcome AddCommand(Action<T_In> action, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, keyData, once);
        }

        public Outcome AddCommand(Action<T_In> action, out IKey? key, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, out key, keyData, once);
        }

        public void Dispatch(T_In @in)
        {
            _signal?.Dispatch(@in);
        }
        #endregion

        #region Methods
        protected virtual ISignal<T_In> Factory()
        {
            return new Signal<T_In>();
        }
        #endregion
    }

    public class LazySignal<T_In1, T_In2> : LazySignalBase, ISignal<T_In1, T_In2>
    {
        #region Fields
        private ISignal<T_In1, T_In2> _signal;
        #endregion

        #region Properties
        protected ISignal<T_In1, T_In2> Signal => _signal ?? (_signal = Factory());
        protected override ISignalBase SignalBase => _signal;
        #endregion

        #region ISignal<T_In1,T_In2> Members
        public Outcome AddCommand(ICommand<T_In1, T_In2> command, IKey? key, bool once = false)
        {
            return Signal.AddCommand(command, key, once);
        }

        public Outcome AddCommand(ICommand<T_In1, T_In2> command, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, keyData, once);
        }

        public Outcome AddCommand(ICommand<T_In1, T_In2> command, out IKey? key, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(command, out key, keyData, once);
        }

        public Outcome AddCommand(Action<T_In1, T_In2> action, IKey? key, bool once = false)
        {
            return Signal.AddCommand(action, key, once);
        }

        public Outcome AddCommand(Action<T_In1, T_In2> action, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, keyData, once);
        }

        public Outcome AddCommand(Action<T_In1, T_In2> action, out IKey? key, object keyData = null, bool once = false)
        {
            return Signal.AddCommand(action, out key, keyData, once);
        }

        public void Dispatch(T_In1 in1, T_In2 in2)
        {
            _signal?.Dispatch(in1, in2);
        }
        #endregion

        #region Methods
        protected virtual ISignal<T_In1, T_In2> Factory()
        {
            return new Signal<T_In1, T_In2>();
        }
        #endregion
    }
}