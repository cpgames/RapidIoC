using System;

namespace cpGames.core.RapidIoC.impl
{
    public class ActionCommand : Command
    {
        #region Fields
        private readonly Action _action;
        #endregion

        #region Constructors
        public ActionCommand(Action action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            _action();
        }

        public static implicit operator ActionCommand(Action action)
        {
            return new ActionCommand(action);
        }
        #endregion
    }

    public class ActionCommand<T_In> : Command<T_In>
    {
        #region Fields
        private readonly Action<T_In> _action;
        #endregion

        #region Constructors
        public ActionCommand(Action<T_In> action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override void Execute(T_In @in)
        {
            _action(@in);
        }

        public static implicit operator ActionCommand<T_In>(Action<T_In> action)
        {
            return new ActionCommand<T_In>(action);
        }
        #endregion
    }

    public class ActionCommand<T_In1, T_In2> : Command<T_In1, T_In2>
    {
        #region Fields
        private readonly Action<T_In1, T_In2> _action;
        #endregion

        #region Constructors
        public ActionCommand(Action<T_In1, T_In2> action)
        {
            _action = action;
        }
        #endregion

        #region Methods
        public override void Execute(T_In1 in1, T_In2 in2)
        {
            _action(in1, in2);
        }

        public static implicit operator ActionCommand<T_In1, T_In2>(Action<T_In1, T_In2> action)
        {
            return new ActionCommand<T_In1, T_In2>(action);
        }
        #endregion
    }
}