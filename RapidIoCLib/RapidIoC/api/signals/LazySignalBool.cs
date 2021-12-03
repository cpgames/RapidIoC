namespace cpGames.core.RapidIoC
{
    public class LazySignalBool : LazySignalResult<bool>, ISignalBool
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        protected override ISignalResult<bool> Factory()
        {
            return new SignalBool();
        }
        #endregion
    }

    public class LazySignalBool<T_In> : LazySignalResult<bool, T_In>, ISignalBool<T_In>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        protected override ISignalResult<bool, T_In> Factory()
        {
            return new SignalBool<T_In>();
        }
        #endregion
    }

    public class LazySignalBool<T_In_1, T_In_2> : LazySignalResult<bool, T_In_1, T_In_2>, ISignalBool<T_In_1, T_In_2>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        protected override ISignalResult<bool, T_In_1, T_In_2> Factory()
        {
            return new SignalBool<T_In_1, T_In_2>();
        }
        #endregion
    }
}