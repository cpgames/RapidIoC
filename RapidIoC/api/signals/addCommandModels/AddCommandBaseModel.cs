namespace cpGames.core.RapidIoC
{
    public interface IAddCommandModel
    {
        #region Properties
        IKey Key { get; }
        IBaseCommand? Command { get; }
        bool Once { get; }
        #endregion
    }

    public abstract class AddCommandBaseModel<T_Command> : IAddCommandModel
        where T_Command : IBaseCommand
    {
        #region Fields
        protected IKey _key = Rapid.InvalidKey;
        protected T_Command? _command;
        protected bool _once;
        #endregion

        #region IAddCommandModel Members
        public IKey Key => _key;
        public IBaseCommand? Command => _command;
        public bool Once => _once;
        #endregion
    }
}