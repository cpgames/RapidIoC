using System.Collections.Generic;
using System.Reflection;

namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Binding is a map of views with properties matching a unique BindingKey.
    /// Binding handles updating values of mapped properties.
    /// </summary>
    public interface IBinding
    {
        #region Properties
        /// <summary>
        /// Unique key.
        /// </summary>
        IKey? Key { get; }
        
        /// <summary>
        /// Map of subscribing views and their property injecting the binding.
        /// </summary>
        IEnumerable<KeyValuePair<IView, PropertyInfo>> Subscribers { get; }

        /// <summary>
        /// If there are no subscribers, this is true.
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// When binding is removed it notifies owning collection via this signal.
        /// </summary>
        ISignal RemovedSignal { get; }

        /// <summary>
        /// When value is updated, this signal is emitted.
        /// </summary>
        ISignal ValueUpdatedSignal { get; }

        /// <summary>
        /// If unbinded, but still has some subscribers left, it is moved to discarded list and no new subscribers can be added.
        /// </summary>
        bool Discarded { get; set; }
        #endregion

        #region Methods
        Outcome GetValue<TValue>(out TValue value);
        Outcome SetValue(object value);

        /// <summary>
        /// Register a property belonging to a view with this binding.
        /// </summary>
        /// <param name="view">View instance owning the property</param>
        /// <param name="property">PropertyInfo of the the property.</param>
        /// <returns>False if view instance was already binded, otherwise true.</returns>
        Outcome Subscribe(IView view, PropertyInfo property);

        /// <summary>
        /// Unregister view.
        /// </summary>
        /// <param name="view">View instance to unregister.</param>
        /// <returns>False if view instance was not registered, otherwise true.</returns>
        Outcome Unsubscribe(IView view);
        Outcome Consume(IBinding binding);
        #endregion
    }
}