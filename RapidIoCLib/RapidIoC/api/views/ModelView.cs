namespace cpGames.core.RapidIoC
{
    public abstract class ModelView<TModel>
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
                UpdateModel();
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

        #region Methods
        protected virtual void UpdateModel() { }
        #endregion
    }
}