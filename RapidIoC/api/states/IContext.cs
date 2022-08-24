namespace cpGames.core.RapidIoC
{
    public interface IContextBase { }

    public interface IContext<T> : IContextBase where T : IStateBase
    {
        #region Properties
        T State { get; set; }
        #endregion

        #region Methods
        void SetState<U>() where U : T;
        #endregion
    }
}