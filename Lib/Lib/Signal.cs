using System;
using System.Linq;

namespace cpGames.core.RapidMVC
{
    public class Signal
    {
        #region Fields
        public Action listener = delegate { };
        public Action onceListener = delegate { };
        #endregion

        #region Methods
        public void AddListener(Action callback)
        {
            listener = AddUnique(listener, callback);
        }

        public void AddOnce(Action callback)
        {
            onceListener = AddUnique(onceListener, callback);
        }

        public void RemoveListener(Action callback)
        {
            listener -= callback;
        }

        public void Dispatch()
        {
            listener();
            onceListener();
            onceListener = delegate { };
        }

        private Action AddUnique(Action listeners, Action callback)
        {
            if (!listeners.GetInvocationList().Contains(callback))
            {
                listeners += callback;
            }
            return listeners;
        }
        #endregion
    }

    public class Signal<T>
    {
        #region Fields
        public Action<T> listener = delegate { };
        public Action<T> onceListener = delegate { };
        #endregion

        #region Methods
        public void AddListener(Action<T> callback)
        {
            listener = AddUnique(listener, callback);
        }

        public void AddOnce(Action<T> callback)
        {
            onceListener = AddUnique(onceListener, callback);
        }

        public void RemoveListener(Action<T> callback)
        {
            listener -= callback;
        }

        public void Dispatch(T type1)
        {
            listener(type1);
            onceListener(type1);
            onceListener = delegate { };
        }

        private Action<T> AddUnique(Action<T> listeners, Action<T> callback)
        {
            if (!listeners.GetInvocationList().Contains(callback))
            {
                listeners += callback;
            }
            return listeners;
        }
        #endregion
    }

    public class Signal<T, U>
    {
        #region Fields
        public Action<T, U> listener = delegate { };
        public Action<T, U> onceListener = delegate { };
        #endregion

        #region Methods
        public void AddListener(Action<T, U> callback)
        {
            listener = AddUnique(listener, callback);
        }

        public void AddOnce(Action<T, U> callback)
        {
            onceListener = AddUnique(onceListener, callback);
        }

        public void RemoveListener(Action<T, U> callback)
        {
            listener -= callback;
        }

        public void Dispatch(T type1, U type2)
        {
            listener(type1, type2);
            onceListener(type1, type2);
            onceListener = delegate { };
        }

        private Action<T, U> AddUnique(Action<T, U> listeners, Action<T, U> callback)
        {
            if (!listeners.GetInvocationList().Contains(callback))
            {
                listeners += callback;
            }
            return listeners;
        }
        #endregion
    }

    public class Signal<T, U, V>
    {
        #region Fields
        public Action<T, U, V> listener = delegate { };
        public Action<T, U, V> onceListener = delegate { };
        #endregion

        #region Methods
        public void AddListener(Action<T, U, V> callback)
        {
            listener = AddUnique(listener, callback);
        }

        public void AddOnce(Action<T, U, V> callback)
        {
            onceListener = AddUnique(onceListener, callback);
        }

        public void RemoveListener(Action<T, U, V> callback)
        {
            listener -= callback;
        }

        public void Dispatch(T type1, U type2, V type3)
        {
            listener(type1, type2, type3);
            onceListener(type1, type2, type3);
            onceListener = delegate { };
        }

        private Action<T, U, V> AddUnique(Action<T, U, V> listeners, Action<T, U, V> callback)
        {
            if (!listeners.GetInvocationList().Contains(callback))
            {
                listeners += callback;
            }
            return listeners;
        }
        #endregion
    }

    public class Signal<T, U, V, W>
    {
        #region Fields
        public Action<T, U, V, W> listener = delegate { };
        public Action<T, U, V, W> onceListener = delegate { };
        #endregion

        #region Methods
        public void AddListener(Action<T, U, V, W> callback)
        {
            listener = AddUnique(listener, callback);
        }

        public void AddOnce(Action<T, U, V, W> callback)
        {
            onceListener = AddUnique(onceListener, callback);
        }

        public void RemoveListener(Action<T, U, V, W> callback)
        {
            listener -= callback;
        }

        public void Dispatch(T type1, U type2, V type3, W type4)
        {
            listener(type1, type2, type3, type4);
            onceListener(type1, type2, type3, type4);
            onceListener = delegate { };
        }

        private Action<T, U, V, W> AddUnique(Action<T, U, V, W> listeners, Action<T, U, V, W> callback)
        {
            if (!listeners.GetInvocationList().Contains(callback))
            {
                listeners += callback;
            }
            return listeners;
        }
        #endregion
    }
}