using System;
using System.Reflection;

namespace cpGames.core.RapidIoC.impl
{
    public class DefaultInstantiator<T> : IInstantiator<T>
    {
        #region IInstantiator Members
        public bool Create(out T value, out string errorMessage)
        {
            var ctor =
                typeof(T).GetConstructor(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                    null, Type.EmptyTypes, null);
            if (ctor == null)
            {
                value = default;
                errorMessage = $"Type <{typeof(T).Name}> missing empty ctor.";
                return false;
            }
            value = (T)ctor.Invoke(null);
            errorMessage = string.Empty;
            return true;
        }
        #endregion
    }
}