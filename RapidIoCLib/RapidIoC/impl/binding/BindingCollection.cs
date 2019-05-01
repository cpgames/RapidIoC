using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    internal class BindingCollection : IBindingCollection
    {
        #region Fields
        private readonly Dictionary<IKey, IBinding> _bindings = new Dictionary<IKey, IBinding>();
        private readonly Dictionary<IKey, IBinding> _discardedBindings = new Dictionary<IKey, IBinding>();
        #endregion

        #region IBindingCollection Members
        public int BindingCount => _bindings.Count;

        public bool FindBinding(IKey key, bool includeDiscarded, out IBinding binding, out string errorMessage)
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

        public bool BindingExists(IKey key)
        {
            return _bindings.ContainsKey(key);
        }

        public bool Bind(IKey key, out IBinding binding, out string errorMessage)
        {
            if (!FindBinding(key, false, out binding, out errorMessage))
            {
                binding = new Binding(key);
                binding.RemovedSignal.AddCommand(() =>
                {
                    if (_discardedBindings.ContainsKey(key))
                    {
                        _discardedBindings.Remove(key);
                    }
                    if (_bindings.ContainsKey(key))
                    {
                        _bindings.Remove(key);
                    }
                }, key, true);
                _bindings.Add(key, binding);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool Unbind(IKey key, out string errorMessage)
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

        public bool BindValue(IKey key, object value, out string errorMessage)
        {
            if (!Bind(key, out var binding, out errorMessage))
            {
                return false;
            }
            binding.Discarded = false;
            binding.Value = value;
            errorMessage = string.Empty;
            return true;
        }

        public bool MoveBindingFrom(IKey key, IBindingCollection collection, out string errorMessage)
        {
            if (!FindBinding(key, false, out var binding, out errorMessage))
            {
                return false;
            }
            if (!collection.MoveBindingTo(binding, out errorMessage))
            {
                return false;
            }
            binding.RemovedSignal.ClearCommands();
            _bindings.Remove(key);
            return true;
        }

        public bool MoveBindingTo(IBinding binding, out string errorMessage)
        {
            return
                Bind(binding.Key, out var localBinding, out errorMessage) &&
                localBinding.Join(binding, out errorMessage);
        }
        #endregion
    }
}