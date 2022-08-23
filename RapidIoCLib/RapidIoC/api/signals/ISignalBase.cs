using System.Collections.Generic;

namespace cpGames.core.RapidIoC
{
    public interface ISignalBase
    {
        #region Properties
        IEnumerable<KeyValuePair<IKey, SignalCommandModel>> Commands { get; }
        int CommandCount { get; }
        #endregion

        #region Methods
        bool IsScheduledForRemoval(IKey key);
        bool HasKey(object keyData);
        Outcome RemoveCommand(object keyData);
        Outcome RemoveCommand<TCommand>() where TCommand : IBaseCommand;
        #endregion
    }
}