using System;

namespace cpGames.core.RapidIoC
{
    public interface ISignal : ISignalBase
    {
        #region Methods
        IKey AddCommand(Action callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommand command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommand;
        void Dispatch();
        #endregion
    }

    public interface ISignal<T_In> : ISignalBase
    {
        #region Methods
        IKey AddCommand(Action<T_In> callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommand<T_In> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T_In>;
        void Dispatch(T_In @in);
        #endregion
    }

    public interface ISignal<T_In1, T_In2> : ISignalBase
    {
        #region Methods
        IKey AddCommand(Action<T_In1, T_In2> callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommand<T_In1, T_In2> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommand<T_In1, T_In2>;
        void Dispatch(T_In1 in1, T_In2 in2);
        #endregion
    }
}