namespace cpGames.core.RapidIoC
{
    public abstract class CommandView : BaseCommandView, ICommand
    {
        #region ICommand Members
        public void Execute()
        {
            lock (_syncRoot)
            {
                BeginExecute();
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion

        #region Methods
        protected abstract void ExecuteInternal();
        #endregion
    }

    public abstract class CommandView<T_In> : BaseCommandView, ICommand<T_In>
    {
        #region ICommand<T_In> Members
        public void Execute(T_In @in)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                ExecuteInternal(@in);
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion

        #region Methods
        protected abstract void ExecuteInternal(T_In @in);
        #endregion
    }

    public abstract class CommandView<T_In_1, T_In_2> : BaseCommandView, ICommand<T_In_1, T_In_2>
    {
        #region ICommand<T_In_1,T_In_2> Members
        public void Execute(T_In_1 in1, T_In_2 in2)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                ExecuteInternal(in1, in2);
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion

        #region Methods
        protected abstract void ExecuteInternal(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}