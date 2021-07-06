namespace cpGames.core.RapidIoC
{
    public abstract class CommandResultView<T_Result> : BaseCommandView, ICommandResult<T_Result>
    {
        #region Properties
        public T_Result Result { get; protected set; }
        #endregion

        #region ICommandResult<T_Result> Members
        public T_Result Execute()
        {
            lock (_syncRoot)
            {
                BeginExecute();
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
                return Result;
            }
        }
        #endregion
    }

    public abstract class CommandResultView<T_Result, T_In> : BaseCommandView, ICommandResult<T_Result, T_In>
    {
        #region Properties
        public T_Result Result { get; protected set; }
        public T_In In { get; private set; }
        #endregion

        #region ICommandResult<T_Result,T_In> Members
        public T_Result Execute(T_In @in)
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
                return Result;
            }
        }
        #endregion
    }

    public abstract class CommandResultView<T_Result, T_In_1, T_In_2> : BaseCommandView, ICommandResult<T_Result, T_In_1, T_In_2>
    {
        #region Properties
        public T_Result Result { get; protected set; }
        public T_In_1 In1 { get; private set; }
        public T_In_2 In2 { get; private set; }
        #endregion

        #region ICommandResult<T_Result,T_In_1,T_In_2> Members
        public T_Result Execute(T_In_1 in1, T_In_2 in2)
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
                return Result;
            }
        }
        #endregion
    }
}