using System;

namespace cpGames.core.RapidMVC
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        #region Properties
        public object Key { get; }
        #endregion

        #region Constructors
        public InjectAttribute(object key)
        {
            Key = key;
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ContextAttribute : Attribute
    {
        #region Properties
        public string Name { get; }
        #endregion

        #region Constructors
        public ContextAttribute(string name)
        {
            Name = name;
        }
        #endregion
    }
}