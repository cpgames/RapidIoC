namespace cpGames.core.RapidMVC
{
    public abstract class BaseCommand : IBaseCommand
    {
        #region IBaseCommand Members
        public virtual void Connect() { }

        public virtual void Release() { }
        #endregion
    }

    public abstract class Command : BaseCommand, ICommand
    {
        #region ICommand Members
        public abstract void Execute();
        #endregion
    }

    public abstract class Command<T> : BaseCommand, ICommand<T>
    {
        #region ICommand<T> Members
        public abstract void Execute(T type1);
        #endregion
    }

    public abstract class Command<T, U> : BaseCommand, ICommand<T, U>
    {
        #region ICommand<T,U> Members
        public abstract void Execute(T type1, U type2);
        #endregion
    }
}