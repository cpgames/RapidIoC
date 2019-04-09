namespace cpGames.core.RapidMVC
{
    public interface IViewCollection
    {
        #region Properties
        /// <summary>
        /// Number of views in the collection.
        /// </summary>
        int Count { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Register a unique view. Returns false if view instance is already registered.
        /// </summary>
        /// <param name="view">View instance to register.</param>
        /// <param name="errorMessage">If registration fails, this contains a reason why.</param>
        /// <returns>True if view registered, otherwise false. See <see cref="errorMessage" /> for details.</returns>
        bool RegisterView(IView view, out string errorMessage);

        // Unregister view. Returns false if no view instance is registered.
        bool UnregisterView(IView view, out string errorMessage);
        #endregion
    }
}