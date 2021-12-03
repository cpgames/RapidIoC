﻿namespace cpGames.core.RapidIoC
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
        bool FindBinding(IKey key, bool includeDiscarded, out IBinding binding);

        /// <summary>
        /// Find binding by key. Return false if not found.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="includeDiscarded">Search discarded bucket as well.</param>
        /// <param name="binding">Binding instance if found, otherwise null.</param>
        /// <param name="errorMessage">If fails or binding not found, this explains why.</param>
        /// <returns>True if binding found, otherwise false.</returns>
        bool FindBinding(IKey key, bool includeDiscarded, out IBinding binding, out string errorMessage);

        /// <summary>
        /// Check if binding exists.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns>True if binding exists, otherwise false.</returns>
        bool BindingExists(IKey key);

        /// <summary>
        /// If binding does not exist, register new binding. Otherwise return existing one.
        /// If registering a binding with root context while at least one local context contains a binding with matching key,
        /// this will return false.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="binding">Binding instance if found or created, otherwise null.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool Bind(IKey key, out IBinding binding);

        /// <summary>
        /// If binding does not exist, register new binding. Otherwise return existing one.
        /// If registering a binding with root context while at least one local context contains a binding with matching key,
        /// this will return false.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="binding">Binding instance if found or created, otherwise null.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool Bind(IKey key, out IBinding binding, out string errorMessage);

        /// <summary>
        /// Remove binding by key.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool Unbind(IKey key);

        /// <summary>
        /// Remove binding by key.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="errorMessage">If fails or binding does not exist, this explains why.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool Unbind(IKey key, out string errorMessage);

        /// <summary>
        /// Remove all local bindings.
        /// </summary>
        /// <returns>True if success, otherwise false.</returns>
        bool ClearBindings();

        /// <summary>
        /// Remove all local bindings.
        /// </summary>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool ClearBindings(out string errorMessage);

        /// <summary>
        /// Update binding value, update all injected properties of that binding, and notify all owning views.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="value">New value to assign to binding.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool BindValue(IKey key, object value);

        /// <summary>
        /// Update binding value, update all injected properties of that binding, and notify all owning views.
        /// </summary>
        /// <param name="key">Unique key.</param>
        /// <param name="value">New value to assign to binding.</param>
        /// <param name="errorMessage">True if success, otherwise false.</param>
        /// <returns>True if success, otherwise false.</returns>
        bool BindValue(IKey key, object value, out string errorMessage);

        bool MoveBindingFrom(IKey key, IBindingCollection collection);

        bool MoveBindingFrom(IKey key, IBindingCollection collection, out string errorMessage);

        bool MoveBindingTo(IBinding binding);

        bool MoveBindingTo(IBinding binding, out string errorMessage);
        #endregion
    }
}