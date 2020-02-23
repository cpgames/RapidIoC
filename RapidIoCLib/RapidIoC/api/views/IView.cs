namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Views are registered with Contexts.
    /// You can either derive from default View or implement your own
    /// (particularly when your view has its own base class that is not a view).
    /// </summary>
    public interface IView
    {
        #region Properties
        string ContextName { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Register view with context, call this whenever view is created or initialized.
        /// </summary>
        void RegisterWithContext();

        /// <summary>
        /// Unregister view from context, call this whenever view is destroyed.
        /// </summary>
        void UnregisterFromContext();
        #endregion
    }

    public interface IView<TModel> : IView
    {
        #region Properties
        TModel Model { get; set; }
        bool HasModel { get; }
        Signal ModelSetSignal { get; }
        #endregion
    }
}