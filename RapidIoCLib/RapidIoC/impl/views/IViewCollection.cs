namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// A collection of views owned by a context.
    /// </summary>
    public interface IViewCollection
    {
        #region Properties
        /// <summary>
        /// Number of views in the collection.
        /// </summary>
        int ViewCount { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Register a unique view.
        /// </summary>
        /// <param name="view">View instance to register.</param>
        /// <param name="errorMessage">If fails, this explains why</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool RegisterView(IView view, out string errorMessage);

        /// <summary>
        /// Unregister view.
        /// </summary>
        /// <param name="view">View instance to unregister.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>True if successful, otherwise false.</returns>
        bool UnregisterView(IView view, out string errorMessage);

        /// <summary>
        /// Unregister all views.
        /// </summary>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>True if successful, otherwise false</returns>
        bool ClearViews(out string errorMessage);
        #endregion
    }
}