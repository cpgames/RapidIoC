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
}