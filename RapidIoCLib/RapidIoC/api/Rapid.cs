using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Main point of entry to RapidIoC api.
    /// </summary>
    public static class Rapid
    {
        private static readonly object _syncRoot = new object();

        #region Properties
        public static IKeyFactoryCollection KeyFactoryCollection { get; } = new KeyFactoryCollection();
        public static IContextCollection Contexts { get; } = new ContextCollection();
        #endregion

        #region Methods
        public static void Bind(object keyData, object value, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindOrCreateContext(contextName, out var context, out var errorMessage) ||
                    !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                    !context.BindValue(key, value, out errorMessage))
                {
                    throw new Exception(errorMessage);
                }
            }
        }

        public static void Bind<TKeyData>(object value, string contextName = null)
        {
            Bind(typeof(TKeyData), value, contextName);
        }

        public static TKeyDataValue Bind<TKeyDataValue>(string contextName = null)
        {
            var value = new DefaultInstantiator<TKeyDataValue>().Create();
            Bind<TKeyDataValue>(value, contextName);
            return (TKeyDataValue)value;
        }

        public static TValue Bind<TKeyDataInterface, TValue>(string contextName = null) where TValue : TKeyDataInterface
        {
            var value = new DefaultInstantiator<TValue>().Create();
            Bind<TKeyDataInterface>(value, contextName);
            return (TValue)value;
        }

        public static void Unbind(object keyData, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context, out var errorMessage) ||
                !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.Unbind(key, out errorMessage))
                {
                    throw new Exception(errorMessage);
                }
            }
        }

        public static void Unbind<T>(string contextName = null)
        {
            Unbind(typeof(T), contextName);
        }

        public static IBinding GetBinding(object keyData, string contextName = null)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(contextName, out var context, out var errorMessage) ||
                !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.Bind(key, out var binding, out errorMessage))
                {
                    throw new Exception(errorMessage);
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
            return GetBinding(keyData, contextName).Value;
        }

        public static bool TryGetBindingValue<TValue>(object keyData, out TValue value, out string errorMessage, string contextName = null)
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
                errorMessage = string.Format("Binding with key <{0}> exists, but of wrong type: <{1}>. <{2} expected.",
                    keyData, binding.Value.GetType().Name, typeof(TValue).Name);
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

        public static void RegisterView(IView view)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindOrCreateContext(view.ContextName, out var context, out var errorMessage) ||
                !context.RegisterView(view, out errorMessage))
                {
                    throw new Exception(errorMessage);
                }
            }
        }

        public static void UnregisterView(IView view)
        {
            lock (_syncRoot)
            {
                if (!Contexts.FindContext(view.ContextName, out var context, out var errorMessage) ||
                !context.UnregisterView(view, out errorMessage))
                {
                    throw new Exception(errorMessage);
                }
            }
        }
        #endregion
    }
}