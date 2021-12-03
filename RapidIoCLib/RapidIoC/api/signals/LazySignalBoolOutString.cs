namespace cpGames.core.RapidIoC
{
    public class LazySignalBoolOutString : LazySignalBoolOut<string>, ISignalBoolOutString
    {
        #region Properties
        public override string DefaultOut => string.Empty;
        #endregion

        #region Methods
        protected override ISignalResultOut<bool, string> Factory()
        {
            return new SignalBoolOutString();
        }
        #endregion
    }

    public class LazySignalBoolOutString<T_In> : LazySignalBoolOut<T_In, string>, ISignalBoolOutString<T_In>
    {
        #region Properties
        public override string DefaultOut => string.Empty;
        #endregion

        #region Methods
        protected override ISignalResultOut<bool, T_In, string> Factory()
        {
            return new SignalBoolOutString<T_In>();
        }
        #endregion
    }

    public class LazySignalBoolOutString<T_In_1, T_In_2> : LazySignalBoolOut<T_In_1, T_In_2, string>, ISignalBoolOutString<T_In_1, T_In_2>
    {
        #region Properties
        public override string DefaultOut => string.Empty;
        #endregion

        #region Methods
        protected override ISignalResultOut<bool, T_In_1, T_In_2, string> Factory()
        {
            return new SignalBoolOutString<T_In_1, T_In_2>();
        }
        #endregion
    }
}