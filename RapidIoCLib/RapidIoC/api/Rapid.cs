using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Main point of entry to RapidIoC api.
    /// </summary>
    public static class Rapid
    {
        #region Fields
        private static readonly object _syncRoot = new object();
        #endregion

        #region Properties
        public static IKeyFactoryCollection KeyFactoryCollection { get; } = new KeyFactoryCollection();
        public static IContextCollection Contexts { get; } = new ContextCollection();
        #endregion

        #region Methods
        public static bool Bind(object keyData, object value, string contextName, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindOrCreateContext(contextName, out var context, out errorMessage) ||
                    !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !context.BindValue(key, value, out errorMessage))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Bind(object keyData, object value, out string errorMessage)
        {
            return Bind(keyData, value, null, out errorMessage);
        }

        public static bool Bind<TKeyData>(object value, string contextName, out string errorMessage)
        {
            return Bind(typeof(TKeyData), value, contextName, out errorMessage);
        }

        public static bool Bind<TKeyData>(object value, out string errorMessage)
        {
            return Bind(typeof(TKeyData), value, out errorMessage);
        }

        public static bool Bind<TKeyDataValue>(out TKeyDataValue value, string contextName, out string errorMessage)
        {
            var instantiator = new DefaultInstantiator<TKeyDataValue>(); // ToDo: Inject this
            return
                instantiator.Create(out value, out errorMessage) &&
                Bind<TKeyDataValue>(value, contextName, out errorMessage);
        }

        public static bool Bind<TKeyDataValue>(out TKeyDataValue value, out string errorMessage)
        {
            var instantiator = new DefaultInstantiator<TKeyDataValue>(); // ToDo: Inject this
            return
                instantiator.Create(out value, out errorMessage) &&
                Bind<TKeyDataValue>(value, out errorMessage);
        }

        public static bool Bind<TKeyDataValue>(out string errorMessage)
        {
            var instantiator = new DefaultInstantiator<TKeyDataValue>(); // ToDo: Inject this
            return
                instantiator.Create(out var value, out errorMessage) &&
                Bind<TKeyDataValue>(value, out errorMessage);
        }

        public static bool Bind<TKeyDataInterface, TValue>(out TValue value, string contextName, out string errorMessage) where TValue : TKeyDataInterface
        {
            var instantiator = new DefaultInstantiator<TValue>(); // ToDo: Inject this
            return
                instantiator.Create(out value, out errorMessage) &&
                Bind<TKeyDataInterface>(value, contextName, out errorMessage);
        }

        public static bool Bind<TKeyDataInterface, TValue>(out TValue value, out string errorMessage) where TValue : TKeyDataInterface
        {
            var instantiator = new DefaultInstantiator<TValue>(); // ToDo: Inject this
            return
                instantiator.Create(out value, out errorMessage) &&
                Bind<TKeyDataInterface>(value, out errorMessage);
        }

        public static bool Unbind(object keyData, string contextName, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context, out errorMessage) ||
                    !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !context.Unbind(key, out errorMessage))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Unbind(object keyData, out string errorMessage)
        {
            return Unbind(keyData, null, out errorMessage);
        }

        public static bool Unbind<T>(string contextName, out string errorMessage)
        {
            return Unbind(typeof(T), contextName, out errorMessage);
        }

        public static bool Unbind<T>(out string errorMessage)
        {
            return Unbind(typeof(T), out errorMessage);
        }

        public static IBinding GetBinding(object keyData, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context) ||
                    !KeyFactoryCollection.Create(keyData, out var key) ||
                    !context.Bind(key, out var binding))
                {
                    return null;
                }
                return binding;
            }
        }

        public static IBinding GetBinding<T>(string contextName = null)
        {
            return GetBinding(typeof(T), contextName);
        }

        public static object GetBindingValue(object keyData, string contextName = null)
        {
            return GetBinding(keyData, contextName)?.Value;
        }

        public static bool GetBindingValue<TValue>(object keyData, out TValue value, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context) ||
                    !KeyFactoryCollection.Create(keyData, out var key) ||
                    !context.FindBinding(key, false, out var binding))
                {
                    value = default;
                    return false;
                }
                if (binding.Value is TValue)
                {
                    value = (TValue)binding.Value;
                    return true;
                }
                value = default;
                return false;
            }
        }

        public static bool GetBindingValue<TValue>(object keyData, out TValue value, out string errorMessage, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context, out errorMessage) ||
                    !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !context.FindBinding(key, false, out var binding, out errorMessage))
                {
                    value = default;
                    return false;
                }
                if (binding.Value is TValue)
                {
                    value = (TValue)binding.Value;
                    return true;
                }
                value = default;
                errorMessage = $"Binding with key <{key}> exists, but of wrong type: <{binding.Value.GetType().Name}>. <{typeof(TValue).Name} expected.";
                return false;
            }
        }

        public static bool GetBindingValue<TValue>(out TValue value, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context) ||
                    !KeyFactoryCollection.Create(typeof(TValue), out var key) ||
                    !context.FindBinding(key, false, out var binding))
                {
                    value = default;
                    return false;
                }
                if (binding.Value is TValue)
                {
                    value = (TValue)binding.Value;
                    return true;
                }
                value = default;
                return false;
            }
        }

        public static bool GetBindingValue<TValue>(out TValue value, out string errorMessage, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context, out errorMessage) ||
                    !KeyFactoryCollection.Create(typeof(TValue), out var key, out errorMessage) ||
                    !context.FindBinding(key, false, out var binding, out errorMessage))
                {
                    value = default;
                    return false;
                }
                if (binding.Value is TValue)
                {
                    value = (TValue)binding.Value;
                    return true;
                }
                value = default;
                errorMessage = $"Binding with key <{key}> exists, but of wrong type: <{binding.Value.GetType().Name}>. <{typeof(TValue).Name} expected.";
                return false;
            }
        }

        public static T GetBindingValue<T>(object keyData, string contextName = null)
        {
            return (T)GetBindingValue(keyData, contextName);
        }

        public static T GetBindingValue<T>(string contextName = null)
        {
            return (T)GetBindingValue(typeof(T), contextName);
        }

        public static bool RegisterView(IView view, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindOrCreateContext(view.ContextName, out var context, out errorMessage) ||
                    !context.RegisterView(view, out errorMessage))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool UnregisterView(IView view, out string errorMessage)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(view.ContextName, out var context, out errorMessage) ||
                    !context.UnregisterView(view, out errorMessage))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}