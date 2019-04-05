using System.Collections.Generic;
using System.Linq;
using cpGames.core.Serialization;

namespace cpGames.core.RapidMVC
{
    public interface IContext
    {
        #region Properties
        string Name { get; }
        bool IsRoot { get; }
        IBindingCollection Bindings { get; }
        Signal DestroyedSignal { get; }
        #endregion

        #region Methods
        bool RegisterView(IView view, out string errorMessage);
        bool UnregisterView(IView view, out string errorMessage);
        #endregion
    }

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
                    !Bindings.Register(key, out var binding, out errorMessage) ||
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
            if (_views.Count == 0)
            {
                DestroyedSignal.Dispatch();
            }

            errorMessage = string.Empty;
            return true;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("Context <{0}>", Name);
        }
        #endregion
    }
}