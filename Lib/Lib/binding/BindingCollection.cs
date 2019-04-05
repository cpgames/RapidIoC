using System.Collections.Generic;

namespace cpGames.core.RapidMVC
{
    public interface IBindingCollection
    {
        #region Properties
        int Count { get; }
        #endregion

        #region Methods
        bool Find(IBindingKey key, out IBinding binding, out string errorMessage);
        bool Exists(IBindingKey key, out string errorMessage);
        bool Remove(IBindingKey key, out string errorMessage);
        bool FindAndRemove(IBindingKey key, out IBinding binding, out string errorMessage);
        bool Register(IBindingKey key, out IBinding binding, out string errorMessage);
        bool UpdateValue(IBindingKey key, object value, out IBinding binding, out string errorMessage);
        bool UpdateValue(IBindingKey key, object value, out string errorMessage);
        #endregion
    }

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

        public bool FindAndRemove(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (_bindings.TryGetValue(key, out binding))
            {
                _bindings.Remove(key);
                errorMessage = string.Empty;
                return true;
            }
            if (!_owner.IsRoot && Rapid.Contexts.Root.Bindings.FindAndRemove(key, out binding, out errorMessage))
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = string.Format("Binding with key <{0}> not found in context <{1}>.", key, _owner);
            return false;
        }

        public bool Register(IBindingKey key, out IBinding binding, out string errorMessage)
        {
            if (!Find(key, out binding, out errorMessage))
            {
                binding = new Binding(key);
                binding.OnRemoved.AddOnce(() => _bindings.Remove(key));
                _bindings.Add(key, binding);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool UpdateValue(IBindingKey key, object value, out IBinding binding, out string errorMessage)
        {
            if (!Register(key, out binding, out errorMessage))
            {
                return false;
            }
            binding.Value = value;
            errorMessage = string.Empty;
            return true;
        }

        public bool UpdateValue(IBindingKey key, object value, out string errorMessage)
        {
            return UpdateValue(key, value, out _, out errorMessage);
        }
        #endregion
    }
}