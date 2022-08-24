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
        IKey? ContextKey { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Register view with context, call this whenever view is created or initialized.
        /// </summary>
        Outcome RegisterWithContext();

        /// <summary>
        /// Unregister view from context, call this whenever view is destroyed.
        /// </summary>
        Outcome UnregisterFromContext();
        #endregion
    }

    public interface IView<TModel> : IView
    {
        #region Properties
        TModel Model { get; }
        bool HasModel { get; }
        ISignalOutcome<TModel> ModelBeginSetSignal { get; }
        ISignalOutcome ModelEndSetSignal { get; }
        #endregion

        #region Methods
        Outcome SetModel(TModel model);
        #endregion
    }
}