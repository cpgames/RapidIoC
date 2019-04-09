namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Commands are used to execute a single action.
    ///     They need to be added to a signal.
    /// </summary>
    public interface IBaseCommand
    {
        #region Methods
        void Release();
        #endregion
    }

    public interface ICommand : IBaseCommand
    {
        #region Methods
        void Execute();
        #endregion
    }

    public interface ICommand<in T> : IBaseCommand
    {
        #region Methods
        void Execute(T type1);
        #endregion
    }

    public interface ICommand<in T, in U> : IBaseCommand
    {
        #region Methods
        void Execute(T type1, U type2);
        #endregion
    }
}