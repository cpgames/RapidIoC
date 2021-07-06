namespace cpGames.core.RapidIoC
{
    public abstract class CommandResultOutView<T_Result, T_Out> : BaseCommandView, ICommandResultOut<T_Result, T_Out>
    {
        #region Properties
        public T_Result Result { get; protected set; }
        public T_Out Out { get; protected set; }
        #endregion

        #region ICommandResultOut<T_Result,T_Out> Members
        public T_Result Execute(out T_Out @out)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
                @out = Out;
                return Result;
            }
        }
        #endregion
    }

    public abstract class CommandResultOutView<T_Result, T_In, T_Out> : BaseCommandView, ICommandResultOut<T_Result, T_In, T_Out>
    {
        #region Properties
        public T_Result Result { get; protected set; }
        public T_Out Out { get; protected set; }
        public T_In In { get; private set; }
        #endregion

        #region ICommandResultOut<T_Result,T_In,T_Out> Members
        public T_Result Execute(T_In @in, out T_Out @out)
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
                @out = Out;
                return Result;
            }
        }
        #endregion
    }

    public abstract class CommandResultOutView<T_Result, T_In_1, T_In_2, T_Out> : BaseCommandView, ICommandResultOut<T_Result, T_In_1, T_In_2, T_Out>
    {
        #region Properties
        public T_Result Result { get; protected set; }
        public T_Out Out { get; protected set; }
        public T_In_1 In1 { get; private set; }
        public T_In_2 In2 { get; private set; }
        #endregion

        #region ICommandResultOut<T_Result,T_In_1,T_In_2,T_Out> Members
        public T_Result Execute(T_In_1 in1, T_In_2 in2, out T_Out @out)
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
                @out = Out;
                return Result;
            }
        }
        #endregion
    }
}