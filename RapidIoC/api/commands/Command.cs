namespace cpGames.core.RapidIoC
{
    public abstract class Command : BaseCommand, ICommand
    {
        #region ICommand Members
        public abstract void Execute();
        #endregion
    }

    public abstract class Command<T_In> : BaseCommand, ICommand<T_In>
    {
        #region ICommand<T> Members
        public abstract void Execute(T_In @in);
        #endregion
    }

    public abstract class Command<T_In_1, T_In_2> : BaseCommand, ICommand<T_In_1, T_In_2>
    {
        #region ICommand<T,U> Members
        public abstract void Execute(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}