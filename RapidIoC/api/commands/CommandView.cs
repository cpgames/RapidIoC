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
    }

    public abstract class CommandView<T_In> : BaseCommandView, ICommand<T_In>
    {
        #region Properties
        public T_In In { get; private set; }
        #endregion

        #region ICommand<T_In> Members
        public void Execute(T_In @in)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                In = @in;
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion
    }

    public abstract class CommandView<T_In_1, T_In_2> : BaseCommandView, ICommand<T_In_1, T_In_2>
    {
        #region Properties
        public T_In_1 In1 { get; private set; }
        public T_In_2 In2 { get; private set; }
        #endregion

        #region ICommand<TModel1,TModel2> Members
        public void Execute(T_In_1 in1, T_In_2 in2)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                In1 = in1;
                In2 = in2;
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion
    }
}