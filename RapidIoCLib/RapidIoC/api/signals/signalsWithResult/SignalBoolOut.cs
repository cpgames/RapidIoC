namespace cpGames.core.RapidIoC
{
    public class SignalBoolOut<T_Out> : SignalResultOut<bool, T_Out>
    {
        #region Properties
        public override bool DefaultResult => true;
        public override bool StopOnResult => true;
        public override bool TargetResult => false;
        #endregion

        #region Methods
        public override bool ResultEquals(bool a, bool b)
        {
            return a == b;
        }

        public override bool ResultAggregate(bool a, bool b)
        {
            return a && b;
        }
        #endregion
    }

    public class SignalBoolOut<T_In, T_Out> : SignalResultOut<bool, T_In, T_Out>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        public override bool ResultEquals(bool a, bool b)
        {
            return a == b;
        }

        public override bool ResultAggregate(bool a, bool b)
        {
            return a && b;
        }
        #endregion
    }

    public class SignalBoolOut<T_In_1, T_In_2, T_Out> : SignalResultOut<bool, T_In_1, T_In_2, T_Out>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        public override bool ResultEquals(bool a, bool b)
        {
            return a == b;
        }

        public override bool ResultAggregate(bool a, bool b)
        {
            return a && b;
        }
        #endregion
    }
}