namespace cpGames.core.RapidIoC
{
    public abstract class CommandResultView<T_Result> : BaseCommandView, ICommandResult<T_Result>
    {
        #region ICommandResult<T_Result> Members
        public T_Result Execute()
        {
            lock (_syncRoot)
            {
                BeginExecute();
                var result = ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
                return result;
            }
        }
        #endregion

        #region Methods
        protected abstract T_Result ExecuteInternal();
        #endregion
    }

    public abstract class CommandResultView<T_Result, T_In> : BaseCommandView, ICommandResult<T_Result, T_In>
    {
        #region ICommandResult<T_Result,T_In> Members
        public T_Result Execute(T_In @in)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                var result = ExecuteInternal(@in);
                if (!_retain)
                {
                    EndExecute();
                }
                return result;
            }
        }
        #endregion

        #region Methods
        protected abstract T_Result ExecuteInternal(T_In @in);
        #endregion
    }

    public abstract class CommandResultView<T_Result, T_In_1, T_In_2> : BaseCommandView, ICommandResult<T_Result, T_In_1, T_In_2>
    {
        #region ICommandResult<T_Result,T_In_1,T_In_2> Members
        public T_Result Execute(T_In_1 in1, T_In_2 in2)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                var result = ExecuteInternal(in1, in2);
                if (!_retain)
                {
                    EndExecute();
                }
                return result;
            }
        }
        #endregion

        #region Methods
        protected abstract T_Result ExecuteInternal(T_In_1 in1, T_In_2 in2);
        #endregion
    }
}