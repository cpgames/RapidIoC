using System.Collections.Generic;
using System.Reflection;

namespace cpGames.core.RapidMVC.impl
{
    internal class Binding : IBinding
    {
        #region Fields
        private object _value;
        private readonly Dictionary<IView, PropertyInfo> _propertyMap = new Dictionary<IView, PropertyInfo>();
        #endregion

        #region Constructors
        public Binding(IBindingKey key)
        {
            Key = key;
        }
        #endregion

        #region IBinding Members
        public IBindingKey Key { get; }
        public object Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateBindings();
            }
        }

        public bool Empty => Value == null;
        public Signal RemovedSignal { get; } = new Signal();
        public Signal ValueUpdatedSignal { get; } = new Signal();

        public bool RegisterViewProperty(IView view, PropertyInfo property, out string errorMessage)
        {
            if (_propertyMap.ContainsKey(view))
            {
                errorMessage = string.Format("View <{0}> already binded property <{1}>.", view, property.Name);
                return false;
            }
            _propertyMap.Add(view, property);
            if (!Empty)
            {
                property.SetValue(view, Value, null);
                view.PropertyUpdatedSignal.Dispatch(Key);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool UnregisterView(IView view, out string errorMessage)
        {
            if (!_propertyMap.ContainsKey(view))
            {
                errorMessage = string.Format("View <{0}> is not binded to binding with key <{1}>.", view, Key);
                return false;
            }

            _propertyMap.Remove(view);
            if (Empty && _propertyMap.Count == 0)
            {
                RemovedSignal.Dispatch();
            }
            errorMessage = string.Empty;
            return true;
        }
        #endregion

        #region Methods
        private void UpdateBindings()
        {
            foreach (var propertyMap in _propertyMap)
            {
                propertyMap.Value.SetValue(propertyMap.Key, Value, null);
                propertyMap.Key.PropertyUpdatedSignal.Dispatch(Key);
                ValueUpdatedSignal.Dispatch();
            }
        }
        #endregion
    }
}