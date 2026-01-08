namespace cpGames.core.RapidIoC
{
    public interface IStateBase
    {
        #region Methods
        void Start();
        void Stop();

        void SetContext(IContextBase context);
        #endregion
    }

    public interface IState<out TContext> : IStateBase where TContext : IContextBase
    {
        #region Properties
        TContext? Context { get; }
        #endregion
    }
}