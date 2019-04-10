using System.Collections.Generic;

namespace cpGames.core.RapidMVC.impl
{
    internal class BindingCollection : IBindingCollection
    {
        #region Fields
        private readonly Dictionary<IBindingKey, IBinding> _bindings = new Dictionary<IBindingKey, IBinding>();
        #endregion

        #region IBindingCollection Members
        public int BindingCount => _bindings.Count;

        public bool FindBinding(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (!_bindings.TryGetValue(key, out binding))
            {
                errorMessage = string.Format("Binding with key <{0}> not found.", key);
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool BindingExists(IBindingKey key)
        {
            return _bindings.ContainsKey(key);
        }

        public bool Bind(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (!FindBinding(key, out binding, out errorMessage))
            {
                binding = new Binding(key);
                binding.RemovedSignal.AddCommand(() => _bindings.Remove(key), true);
                _bindings.Add(key, binding);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool Unbind(IBindingKey key, out string errorMessage)
        {
            if (!_bindings.ContainsKey(key))
            {
                errorMessage = string.Format("Binding with key <{0}> not found.", key);
                return false;
            }
            _bindings.Remove(key);
            errorMessage = string.Empty;
            return true;
        }

        public bool ClearBindings(out string errorMessage)
        {
            _bindings.Clear();
            errorMessage = string.Empty;
            return true;
        }

        public bool BindValue(IBindingKey key, object value, out string errorMessage)
        {
            if (!Bind(key, out var binding, out errorMessage))
            {
                return false;
            }
            binding.Value = value;
            errorMessage = string.Empty;
            return true;
        }
        #endregion
    }
}