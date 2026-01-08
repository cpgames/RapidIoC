namespace cpGames.core.RapidIoC
{
    public class SignalCommandModel
    {
        #region Properties
        public IBaseCommand Command { get; }
        public bool Once { get; }
        #endregion

        #region Constructors
        public SignalCommandModel(IBaseCommand command, bool once)
        {
            Command = command;
            Once = once;
        }
        #endregion
    }
}