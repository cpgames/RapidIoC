namespace cpGames.core.RapidIoC
{
    public interface ISignalBoolOut<T_Out> : ISignalResultOut<bool, T_Out> { }
    public interface ISignalBoolOut<T_In, T_Out> : ISignalResultOut<bool, T_In, T_Out> { }
    public interface ISignalBoolOut<T_In_1, T_In_2, T_Out> : ISignalResultOut<bool, T_In_1, T_In_2, T_Out> { }
}