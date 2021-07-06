namespace cpGames.core.RapidIoC.impl
{
    public class ActionResultOutCommand<T_Result, T_Out> : CommandResultOut<T_Result, T_Out>, IActionCommand
    {
        #region Nested type: ActionResultDelegate
        public delegate T_Result ActionResultDelegate(out T_Out @out);
        #endregion

        #region Fields
        private readonly ActionResultDelegate _action;
        #endregion

        #region Constructors
        public ActionResultOutCommand(ActionResultDelegate action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override T_Result Execute(out T_Out @out)
        {
            return _action(out @out);
        }
        #endregion
    }

    public class ActionResultOutCommand<T_Result, T_In, T_Out> : CommandResultOut<T_Result, T_In, T_Out>, IActionCommand
    {
        #region Nested type: ActionResultDelegate
        public delegate T_Result ActionResultDelegate(T_In @in, out T_Out @out);
        #endregion

        #region Fields
        private readonly ActionResultDelegate _action;
        #endregion

        #region Constructors
        public ActionResultOutCommand(ActionResultDelegate action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override T_Result Execute(T_In @in, out T_Out @out)
        {
            return _action(@in, out @out);
        }
        #endregion
    }

    public class ActionResultOutCommand<T_Result, T_In_1, T_In_2, T_Out> : CommandResultOut<T_Result, T_In_1, T_In_2, T_Out>, IActionCommand
    {
        #region Nested type: ActionResultDelegate
        public delegate T_Result ActionResultDelegate(T_In_1 in1, T_In_2 in2, out T_Out @out);
        #endregion

        #region Fields
        private readonly ActionResultDelegate _action;
        #endregion

        #region Constructors
        public ActionResultOutCommand(ActionResultDelegate action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override T_Result Execute(T_In_1 in1, T_In_2 in2, out T_Out @out)
        {
            return _action(in1, in2, out @out);
        }
        #endregion
    }
}