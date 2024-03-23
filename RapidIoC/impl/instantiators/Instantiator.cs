using System;
using System.Reflection;

namespace cpGames.core.RapidIoC.impl
{
    public class DefaultInstantiator<T> : IInstantiator<T>
    {
        #region IInstantiator<T> Members
        public Outcome Create(out T? value)
        {
            var ctor =
                typeof(T).GetConstructor(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                    null, Type.EmptyTypes, null);
            if (ctor == null)
            {
                value = default;
                return Outcome.Fail($"Type <{typeof(T).Name}> missing empty ctor.", this);
            }
            value = (T)ctor.Invoke(null);
            return Outcome.Success();
        }
        #endregion
    }
}