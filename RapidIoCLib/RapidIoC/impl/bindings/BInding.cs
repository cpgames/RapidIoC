using System.Collections.Generic;
using System.Reflection;

namespace cpGames.core.RapidIoC.impl
{
    internal class Binding : IBinding
    {
        #region Fields
        private object _value;
        private readonly Dictionary<IView, PropertyInfo> _subscribers = new Dictionary<IView, PropertyInfo>();
        public IEnumerable<KeyValuePair<IView, PropertyInfo>> Subscribers => _subscribers;
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
        public ISignal RemovedSignal { get; } = new Signal();
        public ISignal ValueUpdatedSignal { get; } = new Signal();
        public bool Discarded { get; set; } = true;

        private void SubscribeInternal(IView view, PropertyInfo property)
        {
            _subscribers.Add(view, property);
            if (!Empty)
            {
                property.SetValue(view, Value, null);
                RegisterPotentialSignalMap(view, property);
            }
        }

        public bool Subscribe(IView view, PropertyInfo property)
        {
            if (_subscribers.ContainsKey(view))
            {
                return false;
            }
            SubscribeInternal(view, property);
            return true;
        }

        public bool Subscribe(IView view, PropertyInfo property, out string errorMessage)
        {
            if (_subscribers.ContainsKey(view))
            {
                errorMessage = $"View <{view}> already binded property <{property.Name}>.";
                return false;
            }
            SubscribeInternal(view, property);
            errorMessage = string.Empty;
            return true;
        }

        private void UnsubscribeInternal(IView view, PropertyInfo property)
        {
            UnregisterPotentialSignalMap(view, property);
            _subscribers.Remove(view);
            if (Discarded && Empty)
            {
                RemovedSignal.Dispatch();
            }
        }

        public bool Unsubscribe(IView view)
        {
            if (!_subscribers.TryGetValue(view, out var property))
            {
                return false;
            }
            UnsubscribeInternal(view, property);
            return true;
        }

        public bool Unsubscribe(IView view, out string errorMessage)
        {
            if (!_subscribers.TryGetValue(view, out var property))
            {
                errorMessage = $"View <{view}> is not binded to binding with key <{Key}>.";
                return false;
            }
            UnsubscribeInternal(view, property);
            errorMessage = string.Empty;
            return true;
        }

        public bool Consume(IBinding binding)
        {
            foreach (var subscriber in binding.Subscribers)
            {
                if (_subscribers.ContainsKey(subscriber.Key))
                {
                    return false;
                }
                if (!Subscribe(subscriber.Key, subscriber.Value))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Consume(IBinding binding, out string errorMessage)
        {
            foreach (var subscriber in binding.Subscribers)
            {
                if (_subscribers.ContainsKey(subscriber.Key))
                {
                    errorMessage = $"Can't join binding <{binding.Key}> with <{Key}>, encountered duplicate subscriber <{subscriber.Key}>.";
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
            if (property.PropertyType.IsSubclassOf(typeof(SignalBase)))
            {
                var signal = (SignalBase)property.GetValue(view, null);
                if (signal != null && signal.HasKey(view))
                {
                    signal.RemoveCommand(view);
                }
            }
        }

        private void RegisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(SignalBase)) && Value != null)
            {
                view.ConnectSignalProperty(property);
            }
        }
        #endregion
    }
}