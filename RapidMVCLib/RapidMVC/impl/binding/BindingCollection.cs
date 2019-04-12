using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidMVC.impl
{
    internal class BindingCollection : IBindingCollection
    {
        #region Fields
        private readonly Dictionary<IBindingKey, IBinding> _bindings = new Dictionary<IBindingKey, IBinding>();
        private readonly Dictionary<IBindingKey, IBinding> _discardedBindings = new Dictionary<IBindingKey, IBinding>();
        #endregion

        #region IBindingCollection Members
        public int BindingCount => _bindings.Count;

        public bool FindBinding(IBindingKey key, bool includeDiscarded, out IBinding binding, out string errorMessage)
        {
            if (!_bindings.TryGetValue(key, out binding) &&
                (!includeDiscarded || !_discardedBindings.TryGetValue(key, out binding)))
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
            if (!FindBinding(key, false, out binding, out errorMessage))
            {
                binding = new Binding(key);
                binding.RemovedSignal.AddCommand(() => _discardedBindings.Remove(key), true);
                _bindings.Add(key, binding);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool Unbind(IBindingKey key, out string errorMessage)
        {
            if (!FindBinding(key, true, out var binding, out errorMessage))
            {
                return false;
            }
            if (!binding.Empty)
            {
                binding.Discarded = true;
                _discardedBindings.Add(key, binding);
            }
            _bindings.Remove(key);
            errorMessage = string.Empty;
            return true;
        }

        public bool ClearBindings(out string errorMessage)
        {
            var bindingKeys = _bindings.Keys.ToList();
            foreach (var key in bindingKeys)
            {
                if (!Unbind(key, out errorMessage))
                {
                    return false;
                }
            }
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