namespace cpGames.core.RapidIoC
{
    public interface ISignalResultOut<T_Result, T_Out> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommandResultOut<T_Result, T_Out> command, IKey key, bool once = false);
        Outcome AddCommand(ICommandResultOut<T_Result, T_Out> command, object? keyData = null, bool once = false);
        Outcome AddCommand(ICommandResultOut<T_Result, T_Out> command, out IKey key, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_Out> action, IKey key, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_Out> action, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_Out> action, out IKey key, object? keyData = null, bool once = false);
        T_Result DispatchResult(T_Result startingResult, out T_Out @out);
        T_Result DispatchResult(out T_Out @out);
        #endregion
    }

    public interface ISignalResultOut<T_Result, T_In, T_Out> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, IKey key, bool once = false);
        Outcome AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, object? keyData = null, bool once = false);
        Outcome AddCommand(ICommandResultOut<T_Result, T_In, T_Out> command, out IKey key, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In, T_Out> action, IKey key, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In, T_Out> action, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In, T_Out> action, out IKey key, object? keyData = null, bool once = false);
        T_Result DispatchResult(T_In @in, T_Result startingResult, out T_Out @out);
        T_Result DispatchResult(T_In @in, out T_Out @out);
        #endregion
    }

    public interface ISignalResultOut<T_Result, T_In_1, T_In_2, T_Out> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, IKey key, bool once = false);
        Outcome AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, object? keyData = null, bool once = false);
        Outcome AddCommand(ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out> command, out IKey key, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In_1, T_In_2, T_Out> action, IKey key, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In_1, T_In_2, T_Out> action, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultOutDelegate<T_Result, T_In_1, T_In_2, T_Out> action, out IKey key, object? keyData = null, bool once = false);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult, out T_Out @out);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion
    }
}