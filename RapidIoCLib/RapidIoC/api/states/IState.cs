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

    public interface IState<out T> : IStateBase where T : IContextBase
    {
        #region Properties
        T Context { get; }
        #endregion
    }
}