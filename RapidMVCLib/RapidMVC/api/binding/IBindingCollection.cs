namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Collection of bindings. Typically you want one binding collection/context.
    /// </summary>
    public interface IBindingCollection
    {
        #region Properties
        // Number of bindings.
        int Count { get; }
        #endregion

        #region Methods
        // Find a binding by key. Return false if not found.
        bool Find(IBindingKey key, out IBinding binding, out string errorMessage);

        // Check if binding with key exists.
        bool Exists(IBindingKey key, out string errorMessage);

        // Register new binding with unique key if one does not exist or return existing one.
        // Note: if registering a root binding while Root context contains a binding with matching key, this will return false.
        bool Bind(IBindingKey key, out IBinding binding, out string errorMessage);

        // Remove binding by key. Return false if not found.
        bool Unbind(IBindingKey key, out string errorMessage);

        // Remove all bindings
        void Clear();

        // Update binding value by key and update all injected properties.
        bool BindValue(IBindingKey key, object value, out string errorMessage);
        #endregion
    }
}