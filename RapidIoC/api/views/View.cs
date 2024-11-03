using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <inheritdoc cref="IView" />
    /// <summary>
    ///     Default View
    /// </summary>
    public abstract class View : IView
    {
        #region Fields
        private bool _registered;
        #endregion

        #region IView Members
        public virtual IKey ContextKey => RootKey.Instance;

        public Outcome RegisterWithContext()
        {
            if (_registered)
            {
                return Outcome.Success();
            }
            var registerResult =
                Rapid.RegisterView(this) &&
                RegisterWithContextInternal();
            _registered = registerResult;
            return registerResult;
        }

        public Outcome UnregisterFromContext()
        {
            if (!_registered)
            {
                return Outcome.Success();
            }
            var unregisterResult =
                UnregisterFromContextInternal() &&
                Rapid.UnregisterView(this);
            _registered = !unregisterResult;
            return unregisterResult;
        }
        #endregion

        #region Methods
        protected virtual Outcome RegisterWithContextInternal()
        {
            return Outcome.Success();
        }

        protected virtual Outcome UnregisterFromContextInternal()
        {
            return Outcome.Success();
        }
        #endregion
    }

    public abstract class View<TModel> : View, IView<TModel>
    {
        #region IView<TModel> Members
        public ISignalOutcome<TModel> ModelBeginSetSignal { get; } = new LazySignalOutcome<TModel>();
        public ISignalOutcome ModelEndSetSignal { get; } = new LazySignalOutcome();
        public virtual TModel Model { get; private set; } = default!;

        public Outcome SetModel(TModel model)
        {
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

    public class Element { }

    public interface IBase<T>
    {
        #region Methods
        T Foo();
        #endregion
    }

    public abstract class Base<T> : IBase<T?>
    {
        #region Properties
        public abstract T Element { get; }
        #endregion

        #region IBase<T?> Members
        public abstract T Foo();
        #endregion
    }

    public interface IDerived<T> : IBase<T> { }

    public class Derived : Base<Element>, IDerived<Element>
    {
        #region Properties
        public override Element Element => new();
        #endregion

        #region IDerived<Element> Members
        public override Element Foo()
        {
            return Element;
        }
        #endregion
    }

    public class DerivedNullable : Base<Element?>, IDerived<Element?>
    {
        #region Properties
        public override Element? Element => default;
        #endregion

        #region IDerived<Element?> Members
        public override Element? Foo()
        {
            return Element;
        }
        #endregion
    }

    public class Caller
    {
        #region Fields
        private readonly IDerived<Element> _derived = new Derived();
        #endregion

        #region Methods
        public Element GetElement()
        {
            return _derived.Foo();
        }
        #endregion
    }
}