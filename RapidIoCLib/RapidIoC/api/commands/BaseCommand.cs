namespace cpGames.core.RapidIoC
{
    public abstract class BaseCommand : IBaseCommand
    {
        #region IBaseCommand Members
        public bool Connect(out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }

        public bool Release(out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
        #endregion
    }
}