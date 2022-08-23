namespace cpGames.core.RapidIoC
{
    public abstract class Command : BaseCommand, ICommand
    {
        #region ICommand Members
        public abstract void Execute();
        #endregion
    }

    public abstract class Command<T> : BaseCommand, ICommand<T>
    {
        #region ICommand<T> Members
        public abstract void Execute(T @in);
        #endregion
    }

    public abstract class Command<T, U> : BaseCommand, ICommand<T, U>
    {
        #region ICommand<T,U> Members
        public abstract void Execute(T in1, U in2);
        #endregion
    }
}