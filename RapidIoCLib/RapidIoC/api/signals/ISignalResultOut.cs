using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    public interface ISignalResultOut<T_Result, T_Out> : ISignalBase
    {
        #region Methods
        IKey AddCommand(ActionResultOutCommand<T_Result, T_Out>.ActionResultDelegate callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommandResultOut<T_Result, T_Out> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResultOut<T_Result, T_Out>;
        IKey AddCommand(Action callback, object keyData = null, bool once = false);
        T_Result DispatchResult(T_Result startingResult, out T_Out @out);
        T_Result DispatchResult(out T_Out @out);
        #endregion
    }

    public interface ISignalResultOut<T_Result, T_In, T_Out> : ISignalBase
    {
        #region Methods
        IKey AddCommand(ActionResultOutCommand<T_Result, T_In, T_Out>.ActionResultDelegate callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResultOut<T_Result, T_In, T_Out>;
        IKey AddCommand(Action<T_In> callback, object keyData = null, bool once = false);
        T_Result DispatchResult(T_In @in, T_Result startingResult, out T_Out @out);
        T_Result DispatchResult(T_In @in, out T_Out @out);
        #endregion
    }

    public interface ISignalResultOut<T_Result, T_In_1, T_In_2, T_Out> : ISignalBase
    {
        #region Methods
        IKey AddCommand(ActionResultOutCommand<T_Result, T_In_1, T_In_2, T_Out>.ActionResultDelegate callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out>;
        IKey AddCommand(Action<T_In_1, T_In_2> callback, object keyData = null, bool once = false);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult, out T_Out @out);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion
    }
}