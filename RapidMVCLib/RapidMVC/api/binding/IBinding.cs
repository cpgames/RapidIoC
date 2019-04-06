using System.Reflection;

namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Binding is a map of views with properties matching a unique BindingKey.
    ///     Binding handles updating values of mapped properties.
    /// </summary>
    public interface IBinding
    {
        #region Properties
        // Unique binding key
        IBindingKey Key { get; }
        // Value dynamically set to registered properties
        object Value { get; set; }
        // If value is null, it is considered empty.
        bool Empty { get; }
        // If there are no properties registered with this binding, and it is Empty, binding will be automatically removed emitting this signal.
        Signal RemovedSignal { get; }
        // Whenever value is updated, this signal is emitted. You don't need to manually update properties that were binded.
        Signal ValueUpdatedSignal { get; }
        #endregion

        #region Methods
        // Register a property belonging to a view with this binding. Returns false if view instance was already binded.
        bool RegisterViewProperty(IView view, PropertyInfo property, out string errorMessage);

        // Unregister view. Returns false if view instance was not registered.
        bool UnregisterView(IView view, out string errorMessage);
        #endregion
    }
}