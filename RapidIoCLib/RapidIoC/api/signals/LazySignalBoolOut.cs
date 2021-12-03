namespace cpGames.core.RapidIoC
{
    public class LazySignalBoolOut<T_Out> : LazySignalResultOut<bool, T_Out>, ISignalBoolOut<T_Out>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        protected override ISignalResultOut<bool, T_Out> Factory()
        {
            return new SignalBoolOut<T_Out>();
        }
        #endregion
    }

    public class LazySignalBoolOut<T_In, T_Out> : LazySignalResultOut<bool, T_In, T_Out>, ISignalBoolOut<T_In, T_Out>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        protected override ISignalResultOut<bool, T_In, T_Out> Factory()
        {
            return new SignalBoolOut<T_In, T_Out>();
        }
        #endregion
    }

    public class LazySignalBoolOut<T_In_1, T_In_2, T_Out> : LazySignalResultOut<bool, T_In_1, T_In_2, T_Out>, ISignalBoolOut<T_In_1, T_In_2, T_Out>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        protected override ISignalResultOut<bool, T_In_1, T_In_2, T_Out> Factory()
        {
            return new SignalBoolOut<T_In_1, T_In_2, T_Out>();
        }
        #endregion
    }
}