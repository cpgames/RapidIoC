using System;

namespace cpGames.core.RapidMVC
{
    /// <summary>
    /// Add this attribute to property you want to be injected.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Optional key to locate the binding injecting this property.
        /// If key is missing, binding will be located by property type
        /// </summary>
        public object Key { get; }
        #endregion

        #region Constructors
        public InjectAttribute() { }

        public InjectAttribute(object key)
        {
            Key = key;
        }
        #endregion
    }

    /// <summary>
    /// Add this to your view to determine context to register a view with.
    /// If missing, view will register with Root context.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ContextAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Optional context name. Root context if missing.
        /// </summary>
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