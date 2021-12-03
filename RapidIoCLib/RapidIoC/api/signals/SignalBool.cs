namespace cpGames.core.RapidIoC
{
    public class SignalBool : SignalResult<bool>, ISignalBool
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

    public class SignalBool<T_In> : SignalResult<bool, T_In>, ISignalBool<T_In>
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

    public class SignalBool<T_In_1, T_In_2> : SignalResult<bool, T_In_1, T_In_2>, ISignalBool<T_In_1, T_In_2>
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
}