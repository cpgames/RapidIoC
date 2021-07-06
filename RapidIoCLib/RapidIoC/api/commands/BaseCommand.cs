namespace cpGames.core.RapidIoC
{
    public abstract class BaseCommand : IBaseCommand
    {
        #region IBaseCommand Members
        public virtual void Connect() { }

        public virtual void Release() { }
        #endregion
    }
}