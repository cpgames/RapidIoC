namespace cpGames.core.RapidIoC
{
    public interface ISignalOutcomeOut<T_Out> : ISignalResultOut<Outcome, T_Out> { }
    public interface ISignalOutcomeOut<T_In, T_Out> : ISignalResultOut<Outcome, T_In, T_Out> { }
    public interface ISignalOutcomeOut<T_In_1, T_In_2, T_Out> : ISignalResultOut<Outcome, T_In_1, T_In_2, T_Out> { }
}