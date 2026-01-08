namespace cpGames.core.RapidIoC
{
    public interface IContextBase { }

    public interface IContext<TState> : IContextBase where TState : IStateBase
    {
        #region Properties
        TState? State { get; set; }
        #endregion

        #region Methods
        void SetState<UState>() where UState : TState;
        #endregion
    }
}