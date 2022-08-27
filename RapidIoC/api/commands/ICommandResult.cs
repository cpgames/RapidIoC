namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Parameterless command with a return value, can be mapped to <see cref="ISignalResult{T_Result}" />.
    /// </summary>
    /// <typeparam name="T_Result">Result</typeparam>
    public interface ICommandResult<out T_Result> : IBaseCommand
    {
        #region Methods
        T_Result Execute();
        #endregion
    }

    /// <summary>
    /// Command with one parameter and a return value, can be mapped to <see cref="ISignalResult{T_Result, T_In}" />.
    /// </summary>
    /// <typeparam name="T_Result">Result</typeparam>
    /// <typeparam name="T_In">Input parameter</typeparam>
    public interface ICommandResult<out T_Result, in T_In> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In @in);
        #endregion
    }

    /// <summary>
    /// Command with two parameters and a return value, can be mapped to <see cref="ISignalResult{T_Result, T_In1, T_In2}" />.
    /// </summary>
    /// <typeparam name="T_Result">Result</typeparam>
    /// <typeparam name="T_In_1">First input parameter</typeparam>
    /// <typeparam name="T_In_2">Second input parameter</typeparam>
    public interface ICommandResult<out T_Result, in T_In_1, in T_In_2> : IBaseCommand
    {
        #region Methods
        T_Result Execute(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}