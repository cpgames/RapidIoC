namespace cpGames.core.RapidIoC
{
    /// <inheritdoc cref="IView" />
    /// <summary>
    /// Default View
    /// </summary>
    public abstract class View : IView
    {
        #region IView Members
        public virtual string ContextName => null;

        public void RegisterWithContext()
        {
            Rapid.RegisterView(this);
        }

        public void UnregisterFromContext()
        {
            Rapid.UnregisterView(this);
        }
        #endregion
    }

    public abstract class View<TModel> : View, IView<TModel>
    {
        #region Fields
        protected TModel _model;
        #endregion

        #region Properties
        public virtual TModel Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateModelInternal();
            }
        }
        #endregion

        #region IView Members
        public bool HasModel => _model != null;
        public Signal ModelSetSignal { get; } = new Signal();
        #endregion

        #region Methods
        private void UpdateModelInternal()
        {
            UpdateModel();
            ModelSetSignal.Dispatch();
        }

        protected virtual void UpdateModel() { }
        #endregion
    }
}