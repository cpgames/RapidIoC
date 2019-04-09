using System.Collections.Generic;
using System.Linq;
using cpGames.core.CpReflection;

namespace cpGames.core.RapidMVC.src
{
    internal class Context : IContext
    {
        #region Fields
        private readonly List<IView> _views = new List<IView>();
        #endregion

        #region Constructors
        public Context(string name)
        {
            Name = name;
            Bindings = new BindingCollection(this);
        }
        #endregion

        #region IContext Members
        public bool IsRoot => Name == ContextCollection.ROOT_CONTEXT_NAME;
        public IBindingCollection Bindings { get; }
        public string Name { get; }
        public Signal DestroyedSignal { get; } = new Signal();

        public bool RegisterView(IView view, out string errorMessage)
        {
            if (_views.Contains(view))
            {
                errorMessage = string.Format("View <{0}> is already registered with context <{1}>.", view, this);
                return false;
            }

            _views.Add(view);

            foreach (var property in view.GetType().GetProperties().Where(x => x.HasAttribute<InjectAttribute>()))
            {
                var keyData = property.GetAttribute<InjectAttribute>().Key ?? property.PropertyType;
                if (!Rapid.BindingKeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !Bindings.Bind(key, out var binding, out errorMessage) ||
                    !binding.RegisterViewProperty(view, property, out errorMessage))
                {
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool UnregisterView(IView view, out string errorMessage)
        {
            if (!_views.Contains(view))
            {
                errorMessage = string.Format("View <{0}> is not registered with context <{1}>.", view, this);
                return false;
            }

            foreach (var property in view.GetType().GetProperties().Where(x => x.HasAttribute<InjectAttribute>()))
            {
                var keyData = property.GetAttribute<InjectAttribute>().Key ?? property.PropertyType;
                if (!Rapid.BindingKeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !Bindings.Find(key, out var binding, out errorMessage) ||
                    !binding.UnregisterView(view, out errorMessage))
                {
                    return false;
                }
            }

            _views.Remove(view);
            DestroyIfEmpty();

            errorMessage = string.Empty;
            return true;
        }

        public bool Bind(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            return Bindings.Bind(key, out binding, out errorMessage);
        }

        public bool BindValue(IBindingKey key, object value, out string errorMessage)
        {
            return Bindings.BindValue(key, value, out errorMessage);
        }

        public bool Unbind(IBindingKey key, out string errorMessage)
        {
            if (!Bindings.Unbind(key, out errorMessage))
            {
                return false;
            }
            DestroyIfEmpty();
            return true;
        }

        public bool Destroy(out string errorMessage)
        {
            Bindings.Clear();
            while (_views.Count > 0)
            {
                if (!UnregisterView(_views[0], out errorMessage))
                {
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }
        #endregion

        #region Methods
        private void DestroyIfEmpty()
        {
            if (_views.Count == 0 && Bindings.Count == 0 && !IsRoot)
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