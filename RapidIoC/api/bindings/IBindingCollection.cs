namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Collection of bindings. Typically you want one binding collection/context.
    /// </summary>
    public interface IBindingCollection
    {
        #region Properties
        /// <summary>
        /// Number of bindings.
        /// </summary>
        int BindingCount { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Find binding by key. Return false if not found.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="includeDiscarded">Search discarded bucket as well.</param>
        /// <param name="binding">Binding instance if found, otherwise null.</param>
        /// <returns>True if binding found, otherwise false.</returns>
        Outcome FindBinding(IKey? key, bool includeDiscarded, out IBinding binding);

        /// <summary>
        /// Check if binding exists.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns>True if binding exists, otherwise false.</returns>
        Outcome BindingExists(IKey? key);

        /// <summary>
        /// If binding does not exist, register new binding. Otherwise return existing one.
        /// If registering a binding with root context while at least one local context contains a binding with matching key,
        /// this will return false.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="binding">Binding instance if found or created, otherwise null.</param>
        /// <returns>True if success, otherwise false.</returns>
        Outcome Bind(IKey? key, out IBinding binding);

        /// <summary>
        /// Remove binding by key.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns>True if success, otherwise false.</returns>
        Outcome Unbind(IKey? key);

        /// <summary>
        /// Remove all local bindings.
        /// </summary>
        /// <returns>True if success, otherwise false.</returns>
        Outcome ClearBindings();

        /// <summary>
        /// Update binding value, update all injected properties of that binding, and notify all owning views.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="value">New value to assign to binding.</param>
        /// <returns>True if success, otherwise false.</returns>
        Outcome BindValue(IKey? key, object value);

        Outcome MoveBindingFrom(IKey? key, IBindingCollection collection);

        Outcome MoveBindingTo(IBinding binding);
        #endregion
    }
}