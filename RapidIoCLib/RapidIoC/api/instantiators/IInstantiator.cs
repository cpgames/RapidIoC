namespace cpGames.core.RapidIoC
{
    public interface IInstantiator<T>
    {
        #region Methods
        Outcome Create(out T value);
        #endregion
    }
}