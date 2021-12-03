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

        public bool FindBinding(IKey key, bool includeDiscarded, out IBinding binding)
        {
            return
                _bindings.TryGetValue(key, out binding) ||
                includeDiscarded && _discardedBindings.TryGetValue(key, out binding);
        }

        public bool FindBinding(IKey key, bool includeDiscarded, out IBinding binding, out string errorMessage)
        {
            if (!FindBinding(key, includeDiscarded, out binding))
            {
                errorMessage = $"Binding with key <{key}> not found.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool BindingExists(IKey key)
        {
            return _bindings.ContainsKey(key);
        }

        public bool Bind(IKey key, out IBinding binding)
        {
            if (!FindBinding(key, false, out binding))
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
            return true;
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

        public bool Unbind(IKey key)
        {
            if (!FindBinding(key, true, out var binding))
            {
                return false;
            }
            UnbindInternal(key, binding);
            return true;
        }

        public bool Unbind(IKey key, out string errorMessage)
        {
            if (FindBinding(key, true, out var binding, out errorMessage))
            {
                UnbindInternal(key, binding);
                return true;
            }
            return true;
        }

        public bool ClearBindings()
        {
            var bindingKeys = _bindings.Keys.ToList();
            return bindingKeys.All(Unbind);
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

        public bool BindValue(IKey key, object value)
        {
            if (!Bind(key, out var binding))
            {
                return false;
            }
            binding.Discarded = false;
            binding.Value = value;
            return true;
        }

        public bool BindValue(IKey key, object value, out string errorMessage)
        {
            if (Bind(key, out var binding, out errorMessage))
            {
                binding.Discarded = false;
                binding.Value = value;
                return true;
            }
            return false;
        }

        public bool MoveBindingFrom(IKey key, IBindingCollection collection)
        {
            if (!FindBinding(key, false, out var binding))
            {
                return false;
            }
            if (!collection.MoveBindingTo(binding))
            {
                return false;
            }
            (binding.RemovedSignal as Signal).ClearCommands();
            _bindings.Remove(key);
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
            (binding.RemovedSignal as Signal).ClearCommands();
            _bindings.Remove(key);
            return true;
        }

        public bool MoveBindingTo(IBinding binding)
        {
            return
                Bind(binding.Key, out var localBinding) &&
                localBinding.Consume(binding);
        }

        public bool MoveBindingTo(IBinding binding, out string errorMessage)
        {
            return
                Bind(binding.Key, out var localBinding, out errorMessage) &&
                localBinding.Consume(binding, out errorMessage);
        }
        #endregion

        #region Methods
        private void UnbindInternal(IKey key, IBinding binding)
        {
            if (!binding.Empty)
            {
                if (binding.Value is SignalBase signal)
                {
                    signal.ClearCommands();
                }
                binding.Discarded = true;
                _discardedBindings.Add(key, binding);
            }
            _bindings.Remove(key);
        }
        #endregion
    }
}