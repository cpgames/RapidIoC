namespace cpGames.core.RapidIoC
{
    public interface IInstantiator<T>
    {
        #region Methods
        bool Create(out T value, out string errorMessage);
        #endregion
    }
}