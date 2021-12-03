namespace cpGames.core.RapidIoC
{
    public interface ISignalBool : ISignalResult<bool> { }
    public interface ISignalBool<T_In> : ISignalResult<bool, T_In> { }
    public interface ISignalBool<T_In_1, T_In_2> : ISignalResult<bool, T_In_1, T_In_2> { }
}