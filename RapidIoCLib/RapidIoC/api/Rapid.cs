using System;
using cpGames.core.RapidIoC.impl;

namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Main point of entry to RapidIoC api.
    /// </summary>
    public static class Rapid
    {
        #region Properties
        public static IKeyFactoryCollection KeyFactoryCollection { get; } = new KeyFactoryCollection();
        public static IContextCollection Contexts { get; } = new ContextCollection();
        #endregion

        #region Methods
        public static void Bind(object keyData, object value, string contextName = null)
        {
            if (!Contexts.FindOrCreateContext(contextName, out var context, out var errorMessage) ||
                !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.BindValue(key, value, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public static void Bind<T>(object value, string contextName = null)
        {
            Bind(typeof(T), value, contextName);
        }

        public static T Bind<T>(string contextName = null)
        {
            var value = new DefaultInstantiator<T>().Create();
            Bind<T>(value, contextName);
            return (T)value;
        }

        public static void Unbind(object keyData, string contextName = null)
        {
            if (!Contexts.FindContext(contextName, out var context, out var errorMessage) ||
                !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.Unbind(key, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public static void Unbind<T>(string contextName = null)
        {
            Unbind(typeof(T), contextName);
        }

        public static IBinding GetBinding(object keyData, string contextName = null)
        {
            if (!Contexts.FindContext(contextName, out var context, out var errorMessage) ||
                !KeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.Bind(key, out var binding, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
            return binding;
        }

        public static IBinding GetBinding<T>(string contextName = null)
        {
            return GetBinding(typeof(T), contextName);
        }

        public static object GetBindingValue(object keyData, string contextName = null)
        {
            return GetBinding(keyData, contextName).Value;
        }

        public static T GetBindingValue<T>(string contextName = null)
        {
            return (T)GetBindingValue(typeof(T), contextName);
        }

        public static void RegisterView(IView view)
        {
            if (!Contexts.FindOrCreateContext(view.ContextName, out var context, out var errorMessage) ||
                !context.RegisterView(view, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public static void UnregisterView(IView view)
        {
            if (!Contexts.FindContext(view.ContextName, out var context, out var errorMessage) ||
                !context.UnregisterView(view, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }
        #endregion
    }
}