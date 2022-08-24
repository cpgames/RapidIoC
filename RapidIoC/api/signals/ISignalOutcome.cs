namespace cpGames.core.RapidIoC
{
    public interface ISignalOutcome : ISignalResult<Outcome> { }
    public interface ISignalOutcome<T_In> : ISignalResult<Outcome, T_In> { }
    public interface ISignalOutcome<T_In_1, T_In_2> : ISignalResult<Outcome, T_In_1, T_In_2> { }
}