using System;

namespace cpGames.core.RapidIoC
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
}