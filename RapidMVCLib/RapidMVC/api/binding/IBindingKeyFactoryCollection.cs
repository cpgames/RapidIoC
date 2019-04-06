namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Contains all binding key factories. Typically you want one of these (Rapid has one already).
    /// </summary>
    public interface IBindingKeyFactoryCollection
    {
        #region Methods
        // Loop through all factories and try to generate a key. Returns false if either a factory fails to create a key or no factory found handling keyData datatype.
        bool Create(object keyData, out IBindingKey key, out string errorMessage);

        // Adds new custom key factory to collection.
        bool AddFactory(IBindingKeyFactory factory, out string errorMessage);
        #endregion
    }
}