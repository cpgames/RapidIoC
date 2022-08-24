using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <inheritdoc cref="IView" />
    /// <summary>
    /// Default View
    /// </summary>
    public abstract class View : IView
    {
        #region IView Members
        public virtual IKey? ContextKey => RootKey.Instance;

        public Outcome RegisterWithContext()
        {
            return Rapid.RegisterView(this);
        }

        public Outcome UnregisterFromContext()
        {
            return Rapid.UnregisterView(this);
        }
        #endregion
    }

    public abstract class View<TModel> : View, IView<TModel>
    {
        #region Properties
        public virtual bool OneTimeSet => !AllowNull;
        public virtual bool AllowNull => false;
        #endregion

        #region IView<TModel> Members
        public ISignalOutcome<TModel> ModelBeginSetSignal { get; } = new LazySignalOutcome<TModel>();
        public ISignalOutcome ModelEndSetSignal { get; } = new LazySignalOutcome();
        public virtual TModel Model { get; private set; }
        public bool HasModel => Model != null;

        public Outcome SetModel(TModel model)
        {
            if (OneTimeSet && HasModel)
            {
                return Outcome.Fail($"View {GetType().Name} is a one-time set, can not update model again.");
            }
            if (ReferenceEquals(Model, model))
            {
                return Outcome.Fail("Model is already set.");
            }
            var beginUpdateModelInternalResult = BeginUpdateModelInternal(model);
            if (!beginUpdateModelInternalResult)
            {
                return beginUpdateModelInternalResult;
            }
            Model = model;
            return EndUpdateModelInternal();
        }
        #endregion

        #region Methods
        private Outcome BeginUpdateModelInternal(TModel newModel)
        {
            return
                ModelBeginSetSignal.DispatchResult(newModel) &&
                BeginUpdateModel(newModel);
        }

        private Outcome EndUpdateModelInternal()
        {
            return
                EndUpdateModel() &&
                ModelEndSetSignal.DispatchResult();
        }

        protected virtual Outcome BeginUpdateModel(TModel newModel)
        {
            return Outcome.Success();
        }

        protected virtual Outcome EndUpdateModel()
        {
            return Outcome.Success();
        }
        #endregion
    }
}