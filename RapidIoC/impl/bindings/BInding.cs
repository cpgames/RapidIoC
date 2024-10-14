using System.Collections.Generic;
using System.Reflection;

namespace cpGames.core.RapidIoC.impl
{
    internal class Binding : IBinding
    {
        #region Fields
        private object? _value;
        private readonly Dictionary<IView, PropertyInfo> _subscribers = new();
        #endregion

        #region Constructors
        public Binding(IKey key)
        {
            Key = key;
        }
        #endregion

        #region IBinding Members
        public IEnumerable<KeyValuePair<IView, PropertyInfo>> Subscribers => _subscribers;
        public IKey Key { get; }

        public bool Empty => _subscribers.Count == 0;
        public ISignal RemovedSignal { get; } = new Signal();
        public ISignal ValueUpdatedSignal { get; } = new Signal();
        public bool Discarded { get; set; } = true;

        public Outcome GetValue<TValue>(out TValue? value)
        {
            if (_value == null)
            {
                value = default;
                return Outcome.Fail("Value is null");
            }
            if (_value is not TValue)
            {
                value = default;
                return Outcome.Fail($"Incorrect value type <{_value.GetType()}>, <{typeof(TValue)}> expected.");
            }
            value = (TValue)_value;
            return Outcome.Success();
        }

        public bool TryGetValue<TValue>(out TValue? value)
        {
            if (_value == null)
            {
                value = default;
                return false;
            }
            if (_value is not TValue)
            {
                value = default;
                return false;
            }
            value = (TValue)_value;
            return true;
        }

        public Outcome SetValue(object value)
        {
            Discarded = false;
            _value = value;
            return UpdateBindings();
        }

        public Outcome Subscribe(IView view, PropertyInfo property)
        {
            if (_subscribers.ContainsKey(view))
            {
                return Outcome.Fail($"View <{view}> already binded property <{property.Name}>.");
            }
            SubscribeInternal(view, property);
            return Outcome.Success();
        }

        public Outcome Unsubscribe(IView view)
        {
            if (!_subscribers.TryGetValue(view, out var property))
            {
                return Outcome.Fail($"View <{view}> is not binded to binding with key <{Key}>.");
            }
            UnsubscribeInternal(view, property);
            return Outcome.Success();
        }

        public Outcome Consume(IBinding binding)
        {
            foreach (var subscriber in binding.Subscribers)
            {
                if (_subscribers.ContainsKey(subscriber.Key))
                {
                    return Outcome.Fail($"Can't consume binding <{binding.Key}> with <{Key}>, encountered duplicate subscriber <{subscriber.Key}>.");
                }
                var subscribeResult = Subscribe(subscriber.Key, subscriber.Value);
                if (!subscribeResult)
                {
                    return subscribeResult;
                }
            }
            return Outcome.Success();
        }
        #endregion

        #region Methods
        private void SubscribeInternal(IView view, PropertyInfo property)
        {
            _subscribers.Add(view, property);
            if (!Empty)
            {
                property.SetValue(view, _value, null);
                RegisterPotentialSignalMap(view, property);
            }
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

        private Outcome UpdateBindings()
        {
            foreach (var propertyMap in _subscribers)
            {
                var view = propertyMap.Key;
                var property = propertyMap.Value;

                var unregisterPotentialSignalMapOutcome = UnregisterPotentialSignalMap(view, property);
                if (!unregisterPotentialSignalMapOutcome)
                {
                    return unregisterPotentialSignalMapOutcome;
                }
                propertyMap.Value.SetValue(propertyMap.Key, _value, null);
                var registerPotentialSignalMapOutcome = RegisterPotentialSignalMap(view, property);
                if (!registerPotentialSignalMapOutcome)
                {
                    return registerPotentialSignalMapOutcome;
                }
                ValueUpdatedSignal.Dispatch();
            }
            return Outcome.Success();
        }

        private Outcome UnregisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(SignalBase)))
            {
                var signal = (SignalBase?)property.GetValue(view, null);
                if (signal != null && signal.HasKey(view))
                {
                    var removeCommandResult = signal.RemoveCommand(view);
                    if (!removeCommandResult)
                    {
                        return removeCommandResult;
                    }
                }
            }
            return Outcome.Success();
        }

        private Outcome RegisterPotentialSignalMap(IView view, PropertyInfo property)
        {
            if (property.PropertyType.IsSubclassOf(typeof(SignalBase)) && _value != null)
            {
                return view.ConnectSignalProperty(property);
            }
            return Outcome.Success();
        }
        #endregion
    }
}