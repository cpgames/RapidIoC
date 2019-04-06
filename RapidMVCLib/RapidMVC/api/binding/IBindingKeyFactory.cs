namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Binding key factory allows creation of binding keys from any type of data. Feel free to add your own.
    /// </summary>
    public interface IBindingKeyFactory
    {
        #region Methods
        // Create new binding key.
        // Note(!): return true with null key if your factory does not handle keyData datatype. BindingKeyFactoryCollection will handle the rest.
        bool Create(object keyData, out IBindingKey key, out string errorMessage);
        #endregion
    }
}