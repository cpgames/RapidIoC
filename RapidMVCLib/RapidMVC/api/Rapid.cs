using System;
using cpGames.core.CpReflection;
using cpGames.core.RapidMVC.src;

namespace cpGames.core.RapidMVC
{
    public static class Rapid
    {
        #region Properties
        public static IBindingKeyFactoryCollection BindingKeyFactoryCollection { get; } = new BindingKeyFactoryCollection();
        public static IContextCollection Contexts { get; } = new ContextCollection();
        #endregion

        #region Methods
        public static void Bind(object keyData, object value, string contextName = null)
        {
            if (!Contexts.FindOrCreate(contextName, out var context, out var errorMessage) ||
                !BindingKeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.BindValue(key, value, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public static void Bind<T>(object value, string contextName = null)
        {
            Bind(typeof(T), value, contextName);
        }

        public static void Bind<T>(string contextName = null)
        {
            Bind<T>(new DefaultInstantiator<T>().Create(), contextName);
        }

        public static void Unbind(object keyData, string contextName = null)
        {
            if (!Contexts.Find(contextName, out var context, out var errorMessage) ||
                !BindingKeyFactoryCollection.Create(keyData, out var key, out errorMessage) ||
                !context.Unbind(key, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public static void Unbind<T>(string contextName = null)
        {
            Unbind(typeof(T), contextName);
        }

        public static void RegisterView(IView view)
        {
            var contextName = view.GetType().GetAttribute<ContextAttribute>()?.Name;
            if (!Contexts.FindOrCreate(contextName, out var context, out var errorMessage) ||
                !context.RegisterView(view, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        public static void UnregisterView(IView view)
        {
            var contextName = view.GetType().GetAttribute<ContextAttribute>()?.Name;
            if (!Contexts.Find(contextName, out var context, out var errorMessage) ||
                !context.UnregisterView(view, out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }
        #endregion
    }
}