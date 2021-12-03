namespace cpGames.core.RapidIoC
{
    public class SignalBoolOutString : SignalBoolOut<string>, ISignalBoolOutString
    {
        #region Properties
        public override string DefaultOut => string.Empty;
        #endregion
    }

    public class SignalBoolOutString<T_In> : SignalBoolOut<T_In, string>, ISignalBoolOutString<T_In>
    {
        #region Properties
        public override string DefaultOut => string.Empty;
        #endregion
    }

    public class SignalBoolOutString<T_In_1, T_In_2> : SignalBoolOut<T_In_1, T_In_2, string>, ISignalBoolOutString<T_In_1, T_In_2>
    {
        #region Properties
        public override string DefaultOut => string.Empty;
        #endregion
    }
}