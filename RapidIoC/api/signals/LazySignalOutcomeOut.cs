namespace cpGames.core.RapidIoC
{
    public abstract class LazySignalOutcomeOut<T_Out> : LazySignalResultOut<Outcome, T_Out>, ISignalOutcomeOut<T_Out>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion
    }

    public abstract class LazySignalOutcomeOut<T_In, T_Out> : LazySignalResultOut<Outcome, T_In, T_Out>, ISignalOutcomeOut<T_In, T_Out>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion
    }

    public abstract class LazySignalOutcomeOut<T_In_1, T_In_2, T_Out> : LazySignalResultOut<Outcome, T_In_1, T_In_2, T_Out>, ISignalOutcomeOut<T_In_1, T_In_2, T_Out>
    {
        #region Properties
        public override Outcome DefaultResult => Outcome.Success();
        #endregion
    }
}