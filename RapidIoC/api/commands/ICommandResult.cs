namespace cpGames.core.RapidIoC
{
    public interface ICommandResult<out T_Result> : IBaseCommand
    {
        #region Methods
        T_Result Execute();
        #endregion
    }

    public interface ICommandResult<out T_Result, in T_In> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In @in);
        #endregion
    }

    public interface ICommandResult<out T_Result, in T_In_1, in T_In_2> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}