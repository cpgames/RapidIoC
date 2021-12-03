namespace cpGames.core.RapidIoC
{
    public interface ISignalResult<T_Result> : ISignalBase
    {
        #region Methods
        IKey AddCommand(ActionResultDelegate<T_Result> callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommandResult<T_Result> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result>;
        T_Result DispatchResult(T_Result startingResult);
        T_Result DispatchResult();
        #endregion
    }

    public interface ISignalResult<T_Result, T_In> : ISignalBase
    {
        #region Methods
        IKey AddCommand(ActionResultDelegate<T_Result, T_In> callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommandResult<T_Result, T_In> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result, T_In>;
        T_Result DispatchResult(T_In @in, T_Result startingResult);
        T_Result DispatchResult(T_In @in);
        #endregion
    }

    public interface ISignalResult<T_Result, T_In_1, T_In_2> : ISignalBase
    {
        #region Methods
        IKey AddCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> callback, object keyData = null, bool once = false);
        IKey AddCommand(ICommandResult<T_Result, T_In_1, T_In_2> command, object keyData = null, bool once = false);
        IKey AddCommand<TCommand>(bool once = false) where TCommand : ICommandResult<T_Result, T_In_1, T_In_2>;
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2, T_Result startingResult);
        T_Result DispatchResult(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}