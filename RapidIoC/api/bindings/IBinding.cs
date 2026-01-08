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
        IKey Key { get; }
        
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
        /// <summary>
        /// Get binding value and cast it to TValue.
        /// </summary>
        /// <typeparam name="TValue">Value type.</typeparam>
        /// <param name="value">Binding value of type TValue, if value is null or mismatched type, then default.</param>
        /// <returns><see cref="Outcome.Success()"/> if value is not null and of type TValue, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome GetValue<TValue>(out TValue? value);

        /// <summary>
        /// Set binding value.
        /// </summary>
        /// <param name="value">Value to set.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome SetValue(object value);

        /// <summary>
        /// Register a property belonging to a view with this binding.
        /// </summary>
        /// <param name="view">View instance owning the property</param>
        /// <param name="property">PropertyInfo of the the property.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome Subscribe(IView view, PropertyInfo property);

        /// <summary>
        /// Unregister view.
        /// </summary>
        /// <param name="view">View instance to unregister.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome Unsubscribe(IView view);

        /// <summary>
        /// Moves all subscribers from another binding into current binding.
        /// </summary>
        /// <param name="binding">Binding to consume.</param>
        /// <returns><see cref="Outcome.Success()"/> if successful, otherwise <see cref="Outcome.Fail(string)"/>.</returns>
        Outcome Consume(IBinding binding);
        #endregion
    }
}