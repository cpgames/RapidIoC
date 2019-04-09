namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Context encapsulates data for a collection of views registered under it.
    ///     Registering view with context automatically binds injected properties.
    /// </summary>
    public interface IContext
    {
        #region Properties
        // Unique name for context.
        string Name { get; }
        // Root context is a global (cross-context) entity. Typically you want one of these.
        bool IsRoot { get; }
        // Collection of bindings belonging to this context.
        IBindingCollection Bindings { get; }
        // If there are no bindings left in the context, it will be automatically deleted.
        Signal DestroyedSignal { get; }
        #endregion

        #region Methods
        // Register a unique view. Returns false if view instance is already registered.
        bool RegisterView(IView view, out string errorMessage);

        // Unregister view. Returns false if no view instance is registered.
        bool UnregisterView(IView view, out string errorMessage);

        // See: IBindingCollection::Bind
        bool Bind(IBindingKey key, out IBinding binding, out string errorMessage);

        // See: IBindingCollection::BindValue
        bool BindValue(IBindingKey key, object value, out string errorMessage);

        // See: IBindingCollection::Unbind
        bool Unbind(IBindingKey key, out string errorMessage);

        // Unregister any views belonging to this context and destroy it.
        bool Destroy(out string errorMessage);
        #endregion
    }
}