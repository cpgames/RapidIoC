namespace cpGames.core.RapidIoC.impl
{
    public class ActionResultCommand<T_Result> : CommandResult<T_Result>
    {
        #region Fields
        private readonly ActionResultDelegate<T_Result> _action;
        #endregion

        #region Constructors
        public ActionResultCommand(ActionResultDelegate<T_Result> action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override T_Result Execute()
        {
            return _action();
        }

        public static implicit operator ActionResultCommand<T_Result>(ActionResultDelegate<T_Result> action)
        {
            return new ActionResultCommand<T_Result>(action);
        }
        #endregion
    }

    public class ActionResultCommand<T_Result, T_In> : CommandResult<T_Result, T_In>
    {
        #region Fields
        private readonly ActionResultDelegate<T_Result, T_In> _action;
        #endregion

        #region Constructors
        public ActionResultCommand(ActionResultDelegate<T_Result, T_In> action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override T_Result Execute(T_In @in)
        {
            return _action(@in);
        }

        public static implicit operator ActionResultCommand<T_Result, T_In>(ActionResultDelegate<T_Result, T_In> action)
        {
            return new ActionResultCommand<T_Result, T_In>(action);
        }
        #endregion
    }

    public class ActionResultCommand<T_Result, T_In_1, T_In_2> : CommandResult<T_Result, T_In_1, T_In_2>
    {
        #region Fields
        private readonly ActionResultDelegate<T_Result, T_In_1, T_In_2> _action;
        #endregion

        #region Constructors
        public ActionResultCommand(ActionResultDelegate<T_Result, T_In_1, T_In_2> action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override T_Result Execute(T_In_1 in1, T_In_2 in2)
        {
            return _action(in1, in2);
        }

        public static implicit operator ActionResultCommand<T_Result, T_In_1, T_In_2>(ActionResultDelegate<T_Result, T_In_1, T_In_2> action)
        {
            return new ActionResultCommand<T_Result, T_In_1, T_In_2>(action);
        }
        #endregion
    }
}