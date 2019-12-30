namespace cpGames.core.RapidIoC
{
    public abstract class ModelView<TModel> : View, IModelView
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

        #region Constructors
        protected ModelView() { }

        protected ModelView(TModel model)
        {
            Model = model;
        }
        #endregion

        #region IModelView Members
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