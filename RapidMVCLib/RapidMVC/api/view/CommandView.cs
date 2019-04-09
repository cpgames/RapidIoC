namespace cpGames.core.RapidMVC
{
    public abstract class BaseCommandView : View, IBaseCommand
    {
        #region IBaseCommand Members
        public virtual void Release()
        {
            Rapid.UnregisterView(this);
        }
        #endregion
    }

    public abstract class CommandView : BaseCommandView, ICommand
    {
        #region ICommand Members
        public abstract void Execute();
        #endregion
    }

    public abstract class CommandView<T> : BaseCommandView, ICommand<T>
    {
        #region ICommand<T> Members
        public abstract void Execute(T type1);
        #endregion
    }

    public abstract class CommandView<T, U> : BaseCommandView, ICommand<T, U>
    {
        #region ICommand<T,U> Members
        public abstract void Execute(T type1, U type2);
        #endregion
    }
}