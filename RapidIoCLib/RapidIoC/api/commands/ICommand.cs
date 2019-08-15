namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Commands are used to execute actions.
    /// They need to be added to a signal, then the signal needs to be dispatched.
    /// </summary>
    public interface IBaseCommand
    {
        #region Methods
        /// <summary>
        /// Any initialization logic when command is connected.
        /// Note: this will only be called once upon connecting to the first signal,
        /// even if same command is connected to multiple signals.
        /// </summary>
        void Connect();

        /// <summary>
        /// Any cleanup logic when command is disconnected from a signal,
        /// e.g. releasing command resources or unregestering from context (see <see cref="BaseCommandView" />).
        /// </summary>
        void Release();
        #endregion
    }

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
    /// <typeparam name="T"></typeparam>
    public interface ICommand<in T> : IBaseCommand
    {
        #region Methods
        void Execute(T type1);
        #endregion
    }

    /// <inheritdoc />
    /// <summary>
    /// Command with two parameters, can be mapped to signal with two parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public interface ICommand<in T, in U> : IBaseCommand
    {
        #region Methods
        void Execute(T type1, U type2);
        #endregion
    }
}