namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Context encapsulates data for a collection of views registered under it.
    /// Registering view with context automatically binds injected properties.
    /// </summary>
    public interface IContext : IViewCollection, IBindingCollection
    {
        #region Properties
        /// <summary>
        /// Unique key for context.
        /// </summary>
        IKey Key { get; }

        /// <summary>
        /// Root context is a global (cross-context) entity. Typically you want one of these.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// If there are no bindings or views left in the context,
        /// it will be automatically deleted and this signal will be dispatched.
        /// </summary>
        ISignal DestroyedSignal { get; }

        /// <summary>
        /// Check if local binding exists.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns>True if binding exists, otherwise false.</returns>
        Outcome LocalBindingExists(IKey key);
        #endregion

        #region Methods
        /// <summary>
        /// Unregister any views belonging to this context and clear local bindings and destroy it.
        /// </summary>
        /// <returns>True if successful, otherwise false.</returns>
        Outcome DestroyContext();
        #endregion
    }
}