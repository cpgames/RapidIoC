namespace cpGames.core.RapidIoC
{
    public abstract class CommandResultOut<T_Result, T_Out> : BaseCommand, ICommandResultOut<T_Result, T_Out>
    {
        #region ICommandResultOut<T_Result,T_Out> Members
        public abstract T_Result Execute(out T_Out @out);
        #endregion
    }

    public abstract class CommandResultOut<T_Result, T_In, T_Out> : BaseCommand, ICommandResultOut<T_Result, T_In, T_Out>
    {
        #region ICommandResultOut<T_Result,T_In,T_Out> Members
        public abstract T_Result Execute(T_In @in, out T_Out @out);
        #endregion
    }
    public abstract class CommandResultOut<T_Result, T_In_1, T_In_2, T_Out> : BaseCommand, ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out>
    {
        #region ICommandResultOut<T_Result,T_In_1,T_In_2,T_Out> Members
        public abstract T_Result Execute(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion
    }
}