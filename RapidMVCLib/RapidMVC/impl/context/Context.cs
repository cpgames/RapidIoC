using System.Linq;
using cpGames.core.CpReflection;

namespace cpGames.core.RapidMVC.impl
{
    internal class Context : IContext
    {
        #region Fields
        private readonly IViewCollection _views = new ViewCollection();
        private readonly IBindingCollection _bindings = new BindingCollection();
        #endregion

        #region Constructors
        public Context(string name)
        {
            Name = name;
        }
        #endregion

        #region IContext Members
        public bool IsRoot => Name == ContextCollection.ROOT_CONTEXT_NAME;
        public string Name { get; }
        public Signal DestroyedSignal { get; } = new Signal();
        public int ViewCount => _views.ViewCount;
        public int BindingCount => _bindings.BindingCount;

        public bool RegisterView(IView view, out string errorMessage)
        {
            if (!_views.RegisterView(view, out errorMessage))
            {
                return false;
            }
            foreach (var property in view.GetType().GetProperties().Where(x => x.HasAttribute<InjectAttribute>()))
            {
                var keyData = property.GetAttribute<InjectAttribute>().Key ?? property.PropertyType;
                if (!Rapid.BindingKeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !Bind(key, out var binding, out errorMessage) ||
                    !binding.RegisterViewProperty(view, property, out errorMessage))
                {
                    return false;
                }
            }
            return true;
        }

        public bool UnregisterView(IView view, out string errorMessage)
        {
            if (!_views.UnregisterView(view, out errorMessage))
            {
                return false;
            }
            foreach (var property in view.GetType().GetProperties().Where(x => x.HasAttribute<InjectAttribute>()))
            {
                var keyData = property.GetAttribute<InjectAttribute>().Key ?? property.PropertyType;
                if (!Rapid.BindingKeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !FindBinding(key, out var binding, out errorMessage) ||
                    !binding.UnregisterView(view, out errorMessage))
                {
                    return false;
                }
            }
            DestroyIfEmpty();
            return true;
        }

        public bool ClearViews(out string errorMessage)
        {
            if (!_views.ClearViews(out errorMessage))
            {
                return false;
            }
            DestroyIfEmpty();
            return true;
        }

        public bool FindBinding(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            return
                !IsRoot && Rapid.Contexts.Root.FindBinding(key, out binding, out errorMessage) ||
                _bindings.FindBinding(key, out binding, out errorMessage);
        }

        public bool BindingExists(IBindingKey key)
        {
            return
                !IsRoot && Rapid.Contexts.Root.BindingExists(key) ||
                _bindings.BindingExists(key);
        }

        public bool Bind(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (IsRoot)
            {
                foreach (var context in Rapid.Contexts.Contexts)
                {
                    if (context.BindingExists(key))
                    {
                        binding = null;
                        errorMessage = string.Format("Binding with key <{0}> is already registered in at least one context <{1}>, " +
                            "it can't be bound to root context until all local bindings matching these key are removed.",
                            key, context);
                        return false;
                    }
                }
            }
            return 
                !IsRoot && Rapid.Contexts.Root.FindBinding(key, out binding, out errorMessage) ||
                _bindings.Bind(key, out binding, out errorMessage);
        }

        public bool BindValue(IBindingKey key, object value, out string errorMessage)
        {
            return _bindings.BindValue(key, value, out errorMessage);
        }

        public bool Unbind(IBindingKey key, out string errorMessage)
        {
            if (!IsRoot && Rapid.Contexts.Root.Unbind(key, out errorMessage) ||
                _bindings.Unbind(key, out errorMessage))
            {
                DestroyIfEmpty();
                return true;
            }
            return false;
        }

        public bool ClearBindings(out string errorMessage)
        {
            if (!_bindings.ClearBindings(out errorMessage))
            {
                return false;
            }
            DestroyIfEmpty();
            return true;
        }

        public bool DestroyContext(out string errorMessage)
        {
            return 
                ClearBindings(out errorMessage) && 
                ClearViews(out errorMessage);
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
            return string.Format("Context <{0}>", Name);
        }
        #endregion
    }
}