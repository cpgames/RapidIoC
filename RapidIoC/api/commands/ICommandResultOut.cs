namespace cpGames.core.RapidIoC
{
    public interface ICommandResultOut<out T_Result, T_Out> : IBaseCommand
    {
        #region Methods
        T_Result Execute(out T_Out @out);
        #endregion
    }

    public interface ICommandResultOut<out T_Result, in T_In, T_Out> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In @in, out T_Out @out);
        #endregion
    }

    public interface ICommandResultOut<out T_Result, in T_In_1, in T_In_2, T_Out> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion
    }
}