namespace cpGames.core.RapidIoC
{
    public abstract class BaseCommandView : View, IBaseCommand
    {
        #region Fields
        private int _connections;
        #endregion

        #region IBaseCommand Members
        public virtual void Connect()
        {
            if (_connections++ == 0)
            {
                RegisterWithContext();
            }
        }

        public virtual void Release()
        {
            if (--_connections == 0)
            {
                UnregisterFromContext();
            }
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