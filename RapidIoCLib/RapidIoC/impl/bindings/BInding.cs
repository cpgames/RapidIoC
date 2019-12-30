using System.Collections.Generic;
using System.Reflection;

namespace cpGames.core.RapidIoC.impl
{
    internal class Binding : IBinding
    {
        #region Fields
        private object _value;
        private readonly Dictionary<IView, PropertyInfo> _subscribers = new Dictionary<IView, PropertyInfo>();
        #endregion

        #region Constructors
        public Binding(IKey key)
        {
            Key = key;
        }
        #endregion

        #region IBinding Members
        public IKey Key { get; }
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
        public bool Discarded { get; set; } = true;

        public bool Subscribe(IView view, PropertyInfo property, out string errorMessage)
        {
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
            if (Discarded && Empty)
            {
                RemovedSignal.Dispatch();
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool Join(IBinding binding, out string errorMessage)
        {
            var bindingImpl = (Binding)binding;
            foreach (var subscriber in bindingImpl._subscribers)
            {
                if (_subscribers.ContainsKey(subscriber.Key))
                {
                    errorMessage = string.Format("Can not join binding <{0}> with <{1}>, encountered duplicate subscriber <{2}>.",
                        binding.Key, Key, subscriber.Key);
                    return false;
                }
                if (!Subscribe(subscriber.Key, subscriber.Value, out errorMessage))
                {
                    return false;
                }
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
                ValueUpdatedSignal.Dispatch();
            }
        }

        private void UnregisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(BaseSignal)))
            {
                var signal = (BaseSignal)property.GetValue(view, null);
                if (signal != null && signal.HasKey(view))
                {
                    signal.RemoveCommand(view);
                }
            }
        }

        private void RegisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(BaseSignal)) && Value != null)
            {
                view.ConnectSignalProperty(property);
            }
        }
        #endregion
    }
}