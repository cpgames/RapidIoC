namespace cpGames.core.RapidIoC
{
    public abstract class CommandResultOutView<T_Result, T_Out> : BaseCommandView, ICommandResultOut<T_Result, T_Out>
    {
        #region ICommandResultOut<T_Result,T_Out> Members
        public T_Result Execute(out T_Out @out)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                var result = ExecuteInternal(out @out);
                if (!_retain)
                {
                    EndExecute();
                }
                return result;
            }
        }
        #endregion

        #region Methods
        protected abstract T_Result ExecuteInternal(out T_Out @out);
        #endregion
    }

    public abstract class CommandResultOutView<T_Result, T_In, T_Out> : BaseCommandView, ICommandResultOut<T_Result, T_In, T_Out>
    {
        #region ICommandResultOut<T_Result,T_In,T_Out> Members
        public T_Result Execute(T_In @in, out T_Out @out)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                var result = ExecuteInternal(@in, out @out);
                if (!_retain)
                {
                    EndExecute();
                }
                return result;
            }
        }
        #endregion

        #region Methods
        protected abstract T_Result ExecuteInternal(T_In @in, out T_Out @out);
        #endregion
    }

    public abstract class CommandResultOutView<T_Result, T_In_1, T_In_2, T_Out> : BaseCommandView, ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out>
    {
        #region ICommandResultOut<T_Result,T_In_1,T_In_2,T_Out> Members
        public T_Result Execute(T_In_1 in1, T_In_2 in2, out T_Out @out)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                var result = ExecuteInternal(in1, in2, out @out);
                if (!_retain)
                {
                    EndExecute();
                }
                return result;
            }
        }
        #endregion

        #region Methods
        protected abstract T_Result ExecuteInternal(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion
    }
}