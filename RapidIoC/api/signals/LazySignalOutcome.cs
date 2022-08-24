namespace cpGames.core.RapidIoC
{
    public class LazySignalOutcome : LazySignalResult<Outcome>, ISignalOutcome
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion

        #region Methods
        protected override ISignalResult<Outcome> Factory()
        {
            return new SignalOutcome();
        }
        #endregion
    }

    public class LazySignalOutcome<T_In> : LazySignalResult<Outcome, T_In>, ISignalOutcome<T_In>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion

        #region Methods
        protected override ISignalResult<Outcome, T_In> Factory()
        {
            return new SignalOutcome<T_In>();
        }
        #endregion
    }

    public class LazySignalOutcome<T_In_1, T_In_2> : LazySignalResult<Outcome, T_In_1, T_In_2>, ISignalOutcome<T_In_1, T_In_2>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion

        #region Methods
        protected override ISignalResult<Outcome, T_In_1, T_In_2> Factory()
        {
            return new SignalOutcome<T_In_1, T_In_2>();
        }
        #endregion
    }
}