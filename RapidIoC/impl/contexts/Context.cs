using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    internal class Context : IContext
    {
        #region Fields
        private readonly IViewCollection _views = new ViewCollection();
        private readonly IBindingCollection _bindings = new BindingCollection();
        #endregion

        #region Constructors
        public Context(IKey? key)
        {
            Key = key;
        }
        #endregion

        #region IContext Members
        public bool IsRoot => RootKey.Instance == Key;
        public IKey? Key { get; }
        public ISignal DestroyedSignal { get; } = new Signal();
        public int ViewCount => _views.ViewCount;
        public int BindingCount => _bindings.BindingCount;

        public Outcome RegisterView(IView view)
        {
            var registerViewOutcome = _views.RegisterView(view);
            if (!registerViewOutcome)
            {
                return registerViewOutcome;
            }

            foreach (var property in view.GetInjectedProperties())
            {
                IBinding binding = null;
                var subscribeOutcome =
                    property.GetInjectionKey(out var key) &&
                    Bind(key, out binding) &&
                    binding.Subscribe(view, property);

                if (!subscribeOutcome)
                {
                    return subscribeOutcome;
                }
            }

            return Outcome.Success();
        }

        public Outcome FindBinding(IKey? key, bool includeDiscarded, out IBinding binding)
        {
            return IsRoot ?
                _bindings.FindBinding(key, includeDiscarded, out binding) :
                Rapid.Contexts.Root.FindBinding(key, includeDiscarded, out binding);
        }

        public Outcome BindingExists(IKey? key)
        {
            return IsRoot ?
                _bindings.BindingExists(key) :
                Rapid.Contexts.Root.BindingExists(key);
        }

        public Outcome Bind(IKey? key, out IBinding binding)
        {
            if (IsRoot)
            {
                foreach (var context in Rapid.Contexts.Contexts
                             .Where(x => x.LocalBindingExists(key)))
                {
                    var moveBindingOutcome = context.MoveBindingFrom(key, this);
                    if (!moveBindingOutcome)
                    {
                        binding = null;
                        return moveBindingOutcome;
                    }
                }
            }
            return FindBinding(key, false, out binding);
        }

        public Outcome BindValue(IKey? key, object value)
        {
            if (IsRoot)
            {
                foreach (var context in Rapid.Contexts.Contexts
                             .Where(x => x.LocalBindingExists(key)))
                {
                    var moveBindingOutcome = context.MoveBindingFrom(key, this);
                    if (!moveBindingOutcome)
                    {
                        return moveBindingOutcome;
                    }
                }
            }
            return _bindings.BindValue(key, value);
        }

        public Outcome MoveBindingFrom(IKey? key, IBindingCollection collection)
        {
            return _bindings.MoveBindingFrom(key, collection);
        }

        public Outcome MoveBindingTo(IBinding binding)
        {
            return _bindings.MoveBindingTo(binding);
        }

        public Outcome Unbind(IKey? key)
        {
            var unbindResult = IsRoot ?
                _bindings.Unbind(key) :
                Rapid.Contexts.Root.Unbind(key);

            if (!unbindResult)
            {
                return unbindResult;
            }

            DestroyIfEmpty();
            return Outcome.Success();
        }

        public Outcome LocalBindingExists(IKey? key)
        {
            return _bindings.BindingExists(key);
        }

        public Outcome UnregisterView(IView view)
        {
            var unregisterViewOutcome = _views.UnregisterView(view);
            if (!unregisterViewOutcome)
            {
                return unregisterViewOutcome;
            }

            foreach (var property in view.GetInjectedProperties())
            {
                IBinding binding = null;
                var unsubscribeOutcome =
                    property.GetInjectionKey(out var key) &&
                    FindBinding(key, true, out binding) &&
                    binding.Unsubscribe(view);
                if (unsubscribeOutcome)
                {
                    return unsubscribeOutcome;
                }
            }
            DestroyIfEmpty();
            return Outcome.Success();
        }

        public Outcome ClearViews()
        {
            var clearViewsOutcome = _views.ClearViews();
            if (!clearViewsOutcome)
            {
                return clearViewsOutcome;
            }
            DestroyIfEmpty();
            return Outcome.Success();
        }

        public Outcome ClearBindings()
        {
            var clearBindingsOutcome = _bindings.ClearBindings();
            if (!clearBindingsOutcome)
            {
                return clearBindingsOutcome;
            }
            DestroyIfEmpty();
            return Outcome.Success();
        }

        public Outcome DestroyContext()
        {
            return
                ClearBindings() &&
                ClearViews();
        }
        #endregion

        #region Methods
        private void DestroyIfEmpty()
        {
            if (ViewCount == 0 && BindingCount == 0 && !IsRoot)
            {
                DestroyedSignal.Dispatch();
            }
        }

        public override string ToString()
        {
            return $"Context <{Key}>";
        }
        #endregion
    }
}