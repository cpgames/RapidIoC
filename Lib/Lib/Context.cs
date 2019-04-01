using System;
using System.Collections.Generic;
using System.Linq;
using cpGames.core.Serialization;

namespace cpGames.core.RapidMVC
{
    public abstract class Context
    {
        #region Fields
        private readonly Dictionary<IBindingKey, IBinding> _bindings = new Dictionary<IBindingKey, IBinding>();
        #endregion

        #region Properties
        public abstract string Name { get; }
        #endregion

        #region Methods
        public void CreateBinding(IBindingKey key, IBinding binding)
        {
            if (_bindings.ContainsKey(key))
            {
                throw new Exception(string.Format("Context <{0}> already contains binding <{1}>.", Name, key));
            }
            _bindings.Add(key, binding);
        }

        public void RemoveBinding(IBindingKey key)
        {
            if (!_bindings.ContainsKey(key))
            {
                throw new Exception(string.Format("Context <{0}> does not contain binding <{1}>.", Name, key));
            }
            _bindings.Remove(key);
        }

        public void RegisterView(IView view)
        {
            foreach (var property in view.GetType().GetProperties().Where(x => x.HasAttribute<InjectAttribute>()))
            {
                var keyData = property.GetAttribute<InjectAttribute>().Key;
                IBindingKey key = null;
                if (keyData is string stringKey)
                {
                    key = new NameBindingKey(stringKey);
                }
                if (key == null)
                {
                    throw new Exception(string.Format("View <{0}> has unknown binding key on property <{1}>", view, property.Name));
                }
                if (_bindings.TryGetValue(key, out var binding))
                {
                    property.SetValue(view, binding.Value, null);
                }
            }
        }
        #endregion
    }
}