namespace cpGames.core.RapidMVC
{
    public interface IBindingKeyFactory
    {
        #region Methods
        bool Create(object keyData, out IBindingKey key, out string errorMessage);
        #endregion
    }
}