namespace cpGames.core.RapidIoC
{
    public class SignalOutcomeOut<T_Out> : SignalResultOut<Outcome, T_Out>, ISignalOutcomeOut<T_Out>
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

    public class SignalOutcomeOut<T_In, T_Out> : SignalResultOut<Outcome, T_In, T_Out>, ISignalOutcomeOut<T_In, T_Out>
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

    public class SignalOutcomeOut<T_In_1, T_In_2, T_Out> : SignalResultOut<Outcome, T_In_1, T_In_2, T_Out>, ISignalOutcomeOut<T_In_1, T_In_2, T_Out>
    {

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