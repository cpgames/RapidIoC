using System;

namespace cpGames.core.RapidIoC
{
    public interface ISignal : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommand command, IKey key, bool once = false);
        Outcome AddCommand(ICommand command, object keyData = null, bool once = false);
        Outcome AddCommand(ICommand command, out IKey key, object keyData = null, bool once = false);
        Outcome AddCommand(Action action, IKey key, bool once = false);
        Outcome AddCommand(Action action, object keyData = null, bool once = false);
        Outcome AddCommand(Action action, out IKey key, object keyData = null, bool once = false);
        void Dispatch();
        #endregion
    }

    public interface ISignal<T_In> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommand<T_In> command, IKey key, bool once = false);
        Outcome AddCommand(ICommand<T_In> command, object keyData = null, bool once = false);
        Outcome AddCommand(ICommand<T_In> command, out IKey key, object keyData = null, bool once = false);
        Outcome AddCommand(Action<T_In> action, IKey key, bool once = false);
        Outcome AddCommand(Action<T_In> action, object keyData = null, bool once = false);
        Outcome AddCommand(Action<T_In> action, out IKey key, object keyData = null, bool once = false);
        void Dispatch(T_In @in);
        #endregion
    }

    public interface ISignal<T_In1, T_In2> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommand<T_In1, T_In2> command, IKey key, bool once = false);
        Outcome AddCommand(ICommand<T_In1, T_In2> command, object keyData = null, bool once = false);
        Outcome AddCommand(ICommand<T_In1, T_In2> command, out IKey key, object keyData = null, bool once = false);
        Outcome AddCommand(Action<T_In1, T_In2> action, IKey key, bool once = false);
        Outcome AddCommand(Action<T_In1, T_In2> action, object keyData = null, bool once = false);
        Outcome AddCommand(Action<T_In1, T_In2> action, out IKey key, object keyData = null, bool once = false);
        void Dispatch(T_In1 in1, T_In2 in2);
        #endregion
    }
}