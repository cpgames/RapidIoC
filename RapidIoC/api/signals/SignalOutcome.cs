namespace cpGames.core.RapidIoC
{
    public class SignalOutcome : SignalResult<Outcome>, ISignalOutcome
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        public override bool StopOnResult => true;
        public override Outcome TargetResult => Outcome.Fail();
        #endregion

        #region Methods
        public override bool ResultEquals(Outcome a, Outcome b)
        {
            return a == b;
        }

        public override Outcome ResultAggregate(Outcome a, Outcome b)
        {
            return a && b;
        }
        #endregion
    }

    public class SignalOutcome<T_In> : SignalResult<Outcome, T_In>, ISignalOutcome<T_In>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        public override bool StopOnResult => true;
        public override Outcome TargetResult => Outcome.Fail();
        #endregion

        #region Methods
        public override bool ResultEquals(Outcome a, Outcome b)
        {
            return a == b;
        }

        public override Outcome ResultAggregate(Outcome a, Outcome b)
        {
            return a && b;
        }
        #endregion
    }

    public class SignalOutcome<T_In_1, T_In_2> : SignalResult<Outcome, T_In_1, T_In_2>, ISignalOutcome<T_In_1, T_In_2>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        public override bool StopOnResult => true;
        public override Outcome TargetResult => Outcome.Fail();
        #endregion

        #region Methods
        public override bool ResultEquals(Outcome a, Outcome b)
        {
            return a == b;
        }

        public override Outcome ResultAggregate(Outcome a, Outcome b)
        {
            return a && b;
        }
        #endregion
    }
}