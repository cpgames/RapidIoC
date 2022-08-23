namespace cpGames.core.RapidIoC
{
    public abstract class BaseCommand : IBaseCommand
    {
        #region IBaseCommand Members
        public virtual Outcome Connect()
        {
            return Outcome.Success();
        }

        public virtual Outcome Release()
        {
            return Outcome.Success();
        }
        #endregion
    }
}