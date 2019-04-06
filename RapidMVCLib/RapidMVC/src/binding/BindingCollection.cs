using System.Collections.Generic;

namespace cpGames.core.RapidMVC.src
{
    internal class BindingCollection : IBindingCollection
    {
        #region Fields
        private readonly IContext _owner;
        private readonly Dictionary<IBindingKey, IBinding> _bindings = new Dictionary<IBindingKey, IBinding>();
        #endregion

        #region Constructors
        public BindingCollection(IContext owner)
        {
            _owner = owner;
        }
        #endregion

        #region IBindingCollection Members
        public int Count => _bindings.Count;

        public bool Find(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (_bindings.TryGetValue(key, out binding) ||
                !_owner.IsRoot && Rapid.Contexts.Root.Bindings.Find(key, out binding, out errorMessage))
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = string.Format("Binding with key <{0}> not found in context <{1}>.", key, _owner);
            return false;
        }

        public bool Exists(IBindingKey key, out string errorMessage)
        {
            if (_bindings.ContainsKey(key) ||
                !_owner.IsRoot && Rapid.Contexts.Root.Bindings.Exists(key, out errorMessage))
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = string.Format("Binding with key <{0}> not found in context <{1}>.", key, _owner);
            return false;
        }

        public bool Remove(IBindingKey key, out string errorMessage)
        {
            if (_bindings.ContainsKey(key))
            {
                _bindings.Remove(key);
                errorMessage = string.Empty;
                return true;
            }
            if (!_owner.IsRoot && Rapid.Contexts.Root.Bindings.Remove(key, out errorMessage))
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = string.Format("Binding with key <{0}> not found in context <{1}>.", key, _owner);
            return false;
        }

        public bool Register(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (_owner.IsRoot)
            {
                foreach (var context in Rapid.Contexts.Contexts)
                {
                    if (context.Bindings.Exists(key, out _))
                    {
                        binding = null;
                        errorMessage = string.Format("Binding with key <{0}> is already registered in at least one context <{1}>, " +
                            "it can't be bound to root context until local binding is removed.", key, context);
                        return false;
                    }
                }
            }

            if (!Find(key, out binding, out errorMessage))
            {
                binding = new Binding(key);
                binding.RemovedSignal.AddOnce(() => _bindings.Remove(key));
                _bindings.Add(key, binding);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool UpdateValue(IBindingKey key, object value, out string errorMessage)
        {
            if (!Register(key, out var binding, out errorMessage))
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