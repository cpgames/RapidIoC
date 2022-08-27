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
        public static IKey RootKey => impl.RootKey.Instance;
        public static IKey InvalidKey => impl.InvalidKey.Instance;
        public static IKeyFactoryCollection KeyFactoryCollection { get; } = new KeyFactoryCollection();
        public static IContextCollection Contexts { get; } = new ContextCollection();
        #endregion

        #region Methods
        public static Outcome Bind(IKey key, IKey contextKey, object value)
        {
            lock (_syncRoot)
            {
                return
                    Contexts.FindOrCreateContext(contextKey, out var context) &&
                    context!.BindValue(key, value);
            }
        }

        public static Outcome Bind(object keyData, IKey contextKey, object value)
        {
            lock (_syncRoot)
            {
                return
                    KeyFactoryCollection.Create(keyData, out var key) &&
                    Bind(key, contextKey, value);
            }
        }

        public static Outcome Bind<TKeyData>(IKey contextKey, object value)
        {
            return
                KeyFactoryCollection.Create(typeof(TKeyData), out var key) &&
                Bind(key, contextKey, value);
        }

        public static Outcome Bind<TDataValue>(IKey contextKey)
        {
            var instantiator = new DefaultInstantiator<TDataValue>(); // ToDo: Inject this
            return
                instantiator.Create(out var value) &&
                Bind<TDataValue>(contextKey, value!);
        }

        public static Outcome Bind<TDataValue>(IKey contextKey, out TDataValue? value)
        {
            var instantiator = new DefaultInstantiator<TDataValue>(); // ToDo: Inject this
            return
                instantiator.Create(out value) &&
                Bind<TDataValue>(contextKey, value!);
        }

        public static Outcome Bind<TKeyDataInterface, TDataValue>(IKey contextKey, out TDataValue? value) where TDataValue : TKeyDataInterface
        {
            var instantiator = new DefaultInstantiator<TDataValue>(); // ToDo: Inject this
            return
                instantiator.Create(out value) &&
                Bind<TKeyDataInterface>(contextKey, value!);
        }

        public static Outcome Unbind(IKey key, IKey contextKey)
        {
            lock (_syncRoot)
            {
                return
                    Contexts.FindContext(contextKey, out var context) &&
                    context!.Unbind(key);
            }
        }

        public static Outcome Unbind<TDataValue>(IKey contextKey)
        {
            return
                KeyFactoryCollection.Create(typeof(TDataValue), out var key) &&
                Unbind(key, contextKey);
        }

        public static Outcome GetBinding(IKey key, IKey contextKey, out IBinding? binding)
        {
            lock (_syncRoot)
            {
                binding = default;
                return
                    Contexts.FindContext(contextKey, out var context) &&
                    context!.Bind(key, out binding);
            }
        }

        public static Outcome GetBinding<TDataValue>(IKey contextKey, out IBinding? binding)
        {
            binding = default;
            return
                KeyFactoryCollection.Create(typeof(TDataValue), out var key) &&
                GetBinding(key, contextKey, out binding);
        }

        public static Outcome GetBindingValue<TDataValue>(IKey key, IKey contextKey, out TDataValue? value)
        {
            lock (_syncRoot)
            {
                value = default;
                IBinding? binding = null;
                return
                    Contexts.FindContext(contextKey, out var context) &&
                    context!.FindBinding(key, false, out binding) &&
                    binding!.GetValue(out value);
            }
        }

        public static Outcome GetBindingValue<TDataValue>(IKey contextKey, out TDataValue? value)
        {
            value = default;
            return
                KeyFactoryCollection.Create(typeof(TDataValue), out var key) &&
                GetBindingValue(key, contextKey, out value);
        }

        public static Outcome RegisterView(IView view)
        {
            lock (_syncRoot)
            {
                return
                    Contexts.FindOrCreateContext(view.ContextKey, out var context) &&
                    context!.RegisterView(view);
            }
        }

        public static Outcome UnregisterView(IView view)
        {
            lock (_syncRoot)
            {
                return
                    Contexts.FindContext(view.ContextKey, out var context) &&
                    context!.UnregisterView(view);
            }
        }
        #endregion
    }
}