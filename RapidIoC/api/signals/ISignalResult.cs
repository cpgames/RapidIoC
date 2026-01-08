namespace cpGames.core.RapidIoC
{
    public interface ISignalResult<T_Result> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommandResult<T_Result> command, IKey key, bool once = false);
        Outcome AddCommand(ICommandResult<T_Result> command, object? keyData = null, bool once = false);
        Outcome AddCommand(ICommandResult<T_Result> command, out IKey key, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result> action, IKey key, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result> action, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result> action, out IKey key, object? keyData = null, bool once = false);
        T_Result DispatchResult(T_Result startingResult);
        T_Result DispatchResult();
        #endregion
    }

    public interface ISignalResult<T_Result, T_In> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommandResult<T_Result, T_In> command, IKey key, bool once = false);
        Outcome AddCommand(ICommandResult<T_Result, T_In> command, object? keyData = null, bool once = false);
        Outcome AddCommand(ICommandResult<T_Result, T_In> command, out IKey key, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result, T_In> action, IKey key, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result, T_In> action, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result, T_In> action, out IKey key, object? keyData = null, bool once = false);
        T_Result DispatchResult(T_In @in, T_Result startingResult);
        T_Result DispatchResult(T_In @in);
        #endregion
    }

    public interface ISignalResult<T_Result, T_In_1, T_In_2> : ISignalBase
    {
        #region Methods
        Outcome AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, IKey key, bool once = false);
        Outcome AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, object? keyData = null, bool once = false);
        Outcome AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, out IKey key, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> action, IKey key, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> action, object? keyData = null, bool once = false);
        Outcome AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> action, out IKey key, object? keyData = null, bool once = false);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}