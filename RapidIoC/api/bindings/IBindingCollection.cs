namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Collection of bindings. Typically you want one binding collection/context.
    /// </summary>
    public interface IBindingCollection
    {
        #region Properties
        /// <summary>
        /// Get number of bindings.
        /// </summary>
        /// <param name="includeDiscarded">Include discarded bindings as well.</param>
        /// <returns>Total number of bindings.</returns>
        int GetBindingCount(bool includeDiscarded);
        #endregion

        #region Methods
        /// <summary>
        /// Find binding by key. Return false if not found.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="includeDiscarded">Search discarded bindings as well.</param>
        /// <param name="binding">Binding instance if found, otherwise null.</param>
        /// <returns><see cref="Outcome.Success()"/> if binding found, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome FindBinding(IKey key, bool includeDiscarded, out IBinding? binding);

        /// <summary>
        /// Check if binding exists.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns><see cref="Outcome.Success()"/> if binding exists, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome BindingExists(IKey key);

        /// <summary>
        /// If binding does not exist, register new binding. Otherwise return existing one.
        /// If registering a binding with root context while at least one local context contains a binding with matching key,
        /// this will return false.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="binding">Binding instance if found or created, otherwise null.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome Bind(IKey key, out IBinding? binding);

        /// <summary>
        /// Remove binding by key.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome Unbind(IKey key);

        /// <summary>
        /// Remove all local bindings.
        /// </summary>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome ClearBindings();

        /// <summary>
        /// Update binding value, update all injected properties of that binding, and notify all owning views.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="value">New value to assign to binding.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome BindValue(IKey key, object value);

        /// <summary>
        /// Move binding from current collection to another collection
        /// </summary>
        /// <param name="key">Binding key</param>
        /// <param name="collection">Destination <see cref="IBindingCollection"/></param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome MoveBindingFrom(IKey key, IBindingCollection collection);

        /// <summary>
        /// Move binding to current collection. Intended to be used with <see cref="MoveBindingFrom"/>.
        /// </summary>
        /// <param name="binding">Binding to move.</param>
        /// <returns></returns>
        Outcome MoveBindingTo(IBinding binding);
        #endregion
    }
}