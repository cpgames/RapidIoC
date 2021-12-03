using System.Collections.Generic;

namespace cpGames.core.RapidIoC
{
    public interface ISignalBase
    {
        #region Properties
        IEnumerable<KeyValuePair<IKey, SignalCommandModel>> Commands { get; }
        int CommandCount { get; }
        #endregion

        bool IsScheduledForRemoval(IKey key);
        bool HasKey(object keyData);
        void RemoveCommand(object keyData, bool silent = false);
        void RemoveCommand<TCommand>(bool silent = false) where TCommand : IBaseCommand;
    }
}