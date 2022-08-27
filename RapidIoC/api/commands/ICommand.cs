namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Parameterless command, can be mapped to <see cref="ISignal"/>.
    /// </summary>
    public interface ICommand : IBaseCommand
    {
        #region Methods
        void Execute();
        #endregion
    }

    /// <summary>
    /// Command with one parameter, can be mapped to <see cref="ISignal{T_In}"/>.
    /// </summary>
    /// <typeparam name="T_In">Input parameter</typeparam>
    public interface ICommand<in T_In> : IBaseCommand
    {
        #region Methods
        void Execute(T_In @in);
        #endregion
    }

    /// <summary>
    /// Command with two parameters, can be mapped to <see cref="ISignal{T_In_1, T_In_2}"/>.
    /// </summary>
    /// <typeparam name="T_In_1">First input parameter</typeparam>
    /// <typeparam name="T_In_2">Second input parameter</typeparam>
    public interface ICommand<in T_In_1, in T_In_2> : IBaseCommand
    {
        #region Methods
        void Execute(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}