namespace cpGames.core.RapidIoC
{
    public abstract class CommandResult<T_Result> : BaseCommand, ICommandResult<T_Result>
    {
        #region ICommandResult<T_Result> Members
        public abstract T_Result Execute();
        #endregion
    }
    public abstract class CommandResult<T_Result, T_In> : BaseCommand, ICommandResult<T_Result, T_In>
    {
        #region ICommandResult<T_Result,T_In> Members
        public abstract T_Result Execute(T_In @in);
        #endregion
    }

    public abstract class CommandResult<T_Result, T_In_1, T_In_2> : BaseCommand, ICommandResult<T_Result, T_In_1, T_In_2>
    {
        #region ICommandResult<T_Result,T_In_1,T_In_2> Members
        public abstract T_Result Execute(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}