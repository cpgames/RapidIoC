namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Command with a return value and an out parameter,
    /// Can be mapped to <see cref="ISignalResultOut{T_Result,T_Out}" />
    /// </summary>
    /// <typeparam name="T_Result">Result</typeparam>
    /// <typeparam name="T_Out">Out parameter</typeparam>
    public interface ICommandResultOut<out T_Result, T_Out> : IBaseCommand
    {
        #region Methods
        T_Result Execute(out T_Out @out);
        #endregion
    }

    /// <summary>
    /// Command with a return value one input and an out parameter,
    /// Can be mapped to <see cref="ISignalResultOut{T_Result,T_In,T_Out}" />
    /// </summary>
    /// <typeparam name="T_Result">Result</typeparam>
    /// <typeparam name="T_In">Input parameter</typeparam>
    /// <typeparam name="T_Out">Out parameter</typeparam>
    public interface ICommandResultOut<out T_Result, in T_In, T_Out> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In @in, out T_Out @out);
        #endregion
    }

    /// <summary>
    /// Command with a return value two inputs and an out parameter,
    /// Can be mapped to <see cref="ISignalResultOut{T_Result,T_In_1,T_In_2,T_Out}" />
    /// </summary>
    /// <typeparam name="T_Result">Result</typeparam>
    /// <typeparam name="T_In_1">First input parameter</typeparam>
    /// <typeparam name="T_In_2">Second input parameter</typeparam>
    /// <typeparam name="T_Out">Out parameter</typeparam>
    public interface ICommandResultOut<out T_Result, in T_In_1, in T_In_2, T_Out> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion
    }
}