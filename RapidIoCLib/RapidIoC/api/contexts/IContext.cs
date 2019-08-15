namespace cpGames.core.RapidIoC
{
    /// <inheritdoc cref="IViewCollection" />
    /// <inheritdoc cref="IBindingCollection" />
    /// <summary>
    /// Context encapsulates data for a collection of views registered under it.
    /// Registering view with context automatically binds injected properties.
    /// </summary>
    public interface IContext : IViewCollection, IBindingCollection
    {
        #region Properties
        /// <summary>
        /// Unique name for context.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Root context is a global (cross-context) entity. Typically you want one of these.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// If there are no bindings or views left in the context,
        /// it will be automatically deleted and this signal will be dispatched.
        /// </summary>
        Signal DestroyedSignal { get; }

        /// <summary>
        /// Check if local binding exists.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns>True if binding exists, otherwise false.</returns>
        bool LocalBindingExists(IKey key);
        #endregion

        #region Methods
        /// <summary>
        /// Unregister any views belonging to this context and clear local bindings and destroy it.
        /// </summary>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool DestroyContext(out string errorMessage);
        #endregion
    }
}