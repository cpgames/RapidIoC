namespace cpGames.core.RapidIoC
{
    public interface ISignalBoolOutString : ISignalBoolOut<string> { }
    public interface ISignalBoolOutString<T_In> : ISignalBoolOut<T_In, string> { }
    public interface ISignalBoolOutString<T_In_1, T_In_2> : ISignalBoolOut<T_In_1, T_In_2, string> { }
}