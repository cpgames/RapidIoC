using System.Collections.Generic;
using System.Reflection;

namespace cpGames.core.RapidMVC.impl
{
    internal class Binding : IBinding
    {
        #region Fields
        private object _value;
        private readonly Dictionary<IView, PropertyInfo> _subscribers = new Dictionary<IView, PropertyInfo>();
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

        public bool Empty => _subscribers.Count == 0;
        public Signal RemovedSignal { get; } = new Signal();
        public Signal ValueUpdatedSignal { get; } = new Signal();
        public bool Discarded { get; set; }

        public bool Subscribe(IView view, PropertyInfo property, out string errorMessage)
        {
            if (Discarded)
            {
                errorMessage = string.Format("Binding <{0}> is discarded and no new subscribers can be added.", Key);
                return false;
            }
            if (_subscribers.ContainsKey(view))
            {
                errorMessage = string.Format("View <{0}> already binded property <{1}>.", view, property.Name);
                return false;
            }
            _subscribers.Add(view, property);
            if (!Empty)
            {
                property.SetValue(view, Value, null);
                RegisterPotentialSignalMap(view, property);
                view.PropertyUpdatedSignal.Dispatch(Key);
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool Unsubscribe(IView view, out string errorMessage)
        {
            if (!_subscribers.TryGetValue(view, out var property))
            {
                errorMessage = string.Format("View <{0}> is not binded to binding with key <{1}>.", view, Key);
                return false;
            }
            UnregisterPotentialSignalMap(view, property);
            _subscribers.Remove(view);
            if (Discarded)
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
            foreach (var propertyMap in _subscribers)
            {
                var view = propertyMap.Key;
                var property = propertyMap.Value;

                UnregisterPotentialSignalMap(view, property);
                propertyMap.Value.SetValue(propertyMap.Key, Value, null);
                RegisterPotentialSignalMap(view, property);
                propertyMap.Key.PropertyUpdatedSignal.Dispatch(Key);
                ValueUpdatedSignal.Dispatch();
            }
        }

        private void UnregisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(BaseSignal)))
            {
                var value = property.GetValue(view, null);
                if (value != null)
                {
                    view.UnregisterSignalMap(property);
                }
            }
        }

        private void RegisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(BaseSignal)) && Value != null)
            {
                view.RegisterSignalMap(property);
            }
        }
        #endregion
    }
}