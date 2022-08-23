using System.Collections.Generic;

namespace cpGames.core.RapidIoC.impl
{
    public abstract class LazySignalBase : ISignalBase
    {
        #region Properties
        protected abstract ISignalBase SignalBase { get; }
        #endregion

        #region ISignalBase Members
        public IEnumerable<KeyValuePair<IKey, SignalCommandModel>> Commands => SignalBase?.Commands;
        public int CommandCount => SignalBase?.CommandCount ?? 0;

        public bool IsScheduledForRemoval(IKey key)
        {
            return SignalBase != null && SignalBase.IsScheduledForRemoval(key);
        }

        public bool HasKey(object keyData)
        {
            return SignalBase != null && SignalBase.HasKey(keyData);
        }

        public Outcome RemoveCommand(object keyData)
        {
            return SignalBase.RemoveCommand(keyData);
        }

        public Outcome RemoveCommand<TCommand>() where TCommand : IBaseCommand
        {
            return SignalBase.RemoveCommand<TCommand>();
        }
        #endregion
    }

    public abstract class LazySignalBaseResult<T_Result> : LazySignalBase
    {
        #region Properties
        public virtual T_Result DefaultResult => default;
        #endregion
    }

    public abstract class LazySignalBaseResultOut<T_Result, T_Out> : LazySignalBaseResult<T_Result>
    {
        #region Properties
        public virtual T_Out DefaultOut => default;
        #endregion
    }
}