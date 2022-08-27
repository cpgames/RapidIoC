using System.Collections.Generic;

namespace cpGames.core.RapidIoC.impl
{
    public abstract class LazySignalBase : ISignalBase
    {
        #region Properties
        protected abstract ISignalBase SignalBase { get; }
        #endregion

        #region ISignalBase Members
        public IEnumerable<KeyValuePair<IKey, SignalCommandModel>> Commands => SignalBase.Commands;
        public int CommandCount => SignalBase.CommandCount;

        public bool IsScheduledForRemoval(IKey key)
        {
            return SignalBase.IsScheduledForRemoval(key);
        }

        public bool HasKey(object keyData)
        {
            return SignalBase.HasKey(keyData);
        }

        public Outcome RemoveCommand(object keyData)
        {
            return SignalBase.RemoveCommand(keyData);
        }

        public Outcome RemoveCommand<TCommand>() where TCommand : IBaseCommand
        {
            return SignalBase.RemoveCommand<TCommand>();
        }

        public Outcome ClearCommands()
        {
            return SignalBase.ClearCommands();
        }
        #endregion
    }

    public abstract class LazySignalBaseResult<T_Result> : LazySignalBase
    {
        #region Properties
        public abstract T_Result DefaultResult { get; }
        #endregion
    }

    public abstract class LazySignalBaseResultOut<T_Result, T_Out> : LazySignalBaseResult<T_Result>
    {
        #region Properties
        public abstract T_Out DefaultOut { get; }
        #endregion
    }
}