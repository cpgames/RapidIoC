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
        /// <returns>True if successful, otherwise false.</returns>
        Outcome RegisterView(IView view);

        /// <summary>
        /// Unregister view.
        /// </summary>
        /// <param name="view">View instance to unregister.</param>
        /// <returns>True if successful, otherwise false.</returns>
        Outcome UnregisterView(IView view);

        /// <summary>
        /// Unregister all views.
        /// </summary>
        /// <returns>True if successful, otherwise false</returns>
        Outcome ClearViews();
        #endregion
    }
}