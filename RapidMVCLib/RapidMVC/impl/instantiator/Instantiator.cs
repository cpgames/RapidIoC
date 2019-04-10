using System;
using System.Reflection;

namespace cpGames.core.RapidMVC.impl
{
    public class DefaultInstantiator<T> : IInstantiator
    {
        #region IInstantiator Members
        public object Create()
        {
            var ctor =
                typeof(T).GetConstructor(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                    null, Type.EmptyTypes, null);
            if (ctor == null)
            {
                throw new Exception(string.Format("Type <{0}> missing empty ctor.", typeof(T).Name));
            }
            return (T)ctor.Invoke(null);
        }
        #endregion
    }
}