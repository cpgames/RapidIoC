namespace cpGames.core.RapidIoC
{
    /// <inheritdoc />
    /// <summary>
    /// Parameterless command, can be mapped to paramaterless signal.
    /// </summary>
    public interface ICommand : IBaseCommand
    {
        #region Methods
        void Execute();
        #endregion
    }

    /// <inheritdoc />
    /// <summary>
    /// Command with one parameter, can be mapped to signal with one parameter.
    /// </summary>
    /// <typeparam name="T_In"></typeparam>
    public interface ICommand<in T_In> : IBaseCommand
    {
        #region Methods
        void Execute(T_In @in);
        #endregion
    }

    /// <inheritdoc />
    /// <summary>
    /// Command with two parameters, can be mapped to signal with two parameters
    /// </summary>
    /// <typeparam name="T_In_1"></typeparam>
    /// <typeparam name="T_In_2"></typeparam>
    public interface ICommand<in T_In_1, in T_In_2> : IBaseCommand
    {
        #region Methods
        void Execute(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}