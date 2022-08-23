namespace cpGames.core.RapidIoC
{
    public class LazySignalOutcomeOut<T_Out> : LazySignalResultOut<Outcome, T_Out>, ISignalOutcomeOut<T_Out>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion

        #region Methods
        protected override ISignalResultOut<Outcome, T_Out> Factory()
        {
            return new SignalOutcomeOut<T_Out>();
        }
        #endregion
    }

    public class LazySignalOutcomeOut<T_In, T_Out> : LazySignalResultOut<Outcome, T_In, T_Out>, ISignalOutcomeOut<T_In, T_Out>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion

        #region Methods
        protected override ISignalResultOut<Outcome, T_In, T_Out> Factory()
        {
            return new SignalOutcomeOut<T_In, T_Out>();
        }
        #endregion
    }

    public class LazySignalOutcomeOut<T_In_1, T_In_2, T_Out> : LazySignalResultOut<Outcome, T_In_1, T_In_2, T_Out>, ISignalOutcomeOut<T_In_1, T_In_2, T_Out>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion

        #region Methods
        protected override ISignalResultOut<Outcome, T_In_1, T_In_2, T_Out> Factory()
        {
            return new SignalOutcomeOut<T_In_1, T_In_2, T_Out>();
        }
        #endregion
    }
}