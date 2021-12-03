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

        public bool RegisterWithContext(out string errorMessage)
        {
            return Rapid.RegisterView(this, out errorMessage);
        }

        public bool UnregisterFromContext(out string errorMessage)
        {
            return Rapid.UnregisterView(this, out errorMessage);
        }
        #endregion
    }

    public abstract class View<TModel> : View, IView<TModel>
    {
        #region Fields
        protected TModel _model;
        #endregion

        #region IView<TModel> Members
        public virtual TModel Model
        {
            get => _model;
            set
            {
                _model = value;
                UpdateModelInternal();
            }
        }
        public bool HasModel => _model != null;
        public ISignal ModelSetSignal { get; } = new LazySignal();
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