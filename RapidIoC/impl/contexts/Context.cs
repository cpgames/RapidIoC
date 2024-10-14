using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    /// <inheritdoc cref="IContext" />
    internal class Context : IContext
    {
        #region Fields
        private readonly IViewCollection _views = new ViewCollection();
        private readonly IBindingCollection _bindings = new BindingCollection();
        #endregion

        #region Constructors
        public Context(IKey key)
        {
            Key = key;
        }
        #endregion

        #region IContext Members
        public bool IsRoot => RootKey.Instance == Key;
        public IKey Key { get; }
        public ISignal DestroyedSignal { get; } = new Signal();
        public int ViewCount => _views.ViewCount;

        public int GetBindingCount(bool includeDiscarded = false)
        {
            return _bindings.GetBindingCount(includeDiscarded);
        }

        public Outcome RegisterView(IView view)
        {
            var registerViewOutcome = _views.RegisterView(view);
            if (!registerViewOutcome)
            {
                return registerViewOutcome;
            }

            foreach (var property in view.GetInjectedProperties())
            {
                IBinding? binding = null;
                var subscribeOutcome =
                    property.GetInjectionKey(out var key) &&
                    Bind(key, out binding) &&
                    binding!.Subscribe(view, property);

                if (!subscribeOutcome)
                {
                    return subscribeOutcome;
                }
            }

            return Outcome.Success();
        }

        public Outcome FindBinding(IKey key, bool includeDiscarded, out IBinding? binding)
        {
            if (!_bindings.BindingExists(key, includeDiscarded) && !IsRoot)
            {
                return Rapid.Contexts.Root.FindBinding(key, includeDiscarded, out binding);
            }
            return _bindings.FindBinding(key, includeDiscarded, out binding);
        }

        public bool BindingExists(IKey key, bool includeDiscarded)
        {
            if (_bindings.BindingExists(key, includeDiscarded))
            {
                return true;
            }
            if (!IsRoot)
            {
                return Rapid.Contexts.Root.BindingExists(key, includeDiscarded);
            }
            return false;
        }

        public Outcome Bind(IKey key, out IBinding? binding)
        {
            if (IsRoot)
            {
                foreach (var context in Rapid.Contexts.Contexts
                             .Where(x => x.LocalBindingExists(key, true)))
                {
                    var moveBindingOutcome = context.MoveBindingFrom(key, this);
                    if (!moveBindingOutcome)
                    {
                        binding = null;
                        return moveBindingOutcome;
                    }
                }
            }
            if (BindingExists(key, false))
            {
                return FindBinding(key, false, out binding);
            }
            return _bindings.Bind(key, out binding);
        }

        public Outcome BindValue(IKey key, object value)
        {
            if (IsRoot)
            {
                foreach (var context in Rapid.Contexts.Contexts
                             .Where(x => x.LocalBindingExists(key, true)))
                {
                    var moveBindingOutcome = context.MoveBindingFrom(key, this);
                    if (!moveBindingOutcome)
                    {
                        return moveBindingOutcome;
                    }
                }
            }
            else
            {
                if (Rapid.Contexts.Root.BindingExists(key, true))
                {
                    return Outcome.Fail($"Binding <{key}> already exists in Root context.");
                }
            }
            return
                Bind(key, out var binding) &&
                binding!.SetValue(value);
        }

        public Outcome MoveBindingFrom(IKey key, IBindingCollection collection)
        {
            return _bindings.MoveBindingFrom(key, collection);
        }

        public Outcome MoveBindingTo(IBinding binding)
        {
            return _bindings.MoveBindingTo(binding);
        }

        public Outcome Unbind(IKey key)
        {
            var unbindResult = _bindings.Unbind(key);
            if (!unbindResult)
            {
                return unbindResult;
            }

            DestroyIfEmpty();
            return Outcome.Success();
        }

        public bool LocalBindingExists(IKey key, bool includeDiscarded)
        {
            return _bindings.BindingExists(key, includeDiscarded);
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
                IBinding? binding = null;
                var findBindingResult =
                    property.GetInjectionKey(out var key) &&
                    FindBinding(key, true, out binding);
                if (!findBindingResult)
                {
                    continue;
                }
                var unsubscribeResult = binding!.Unsubscribe(view);
                if (!unsubscribeResult)
                {
                    return unsubscribeResult;
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
            if (!IsRoot &&
                ViewCount == 0 &&
                GetBindingCount(true) == 0)
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