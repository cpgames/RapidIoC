using System.Reflection;

namespace cpGames.core.RapidMVC
{
    /// <summary>
    /// Binding is a map of views with properties matching a unique BindingKey.
    /// Binding handles updating values of mapped properties.
    /// </summary>
    public interface IBinding
    {
        #region Properties
        /// <summary>
        /// Unique binding key.
        /// </summary>
        IBindingKey Key { get; }

        /// <summary>
        /// Value dynamically set to all registered properties.
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// If value is null, it is considered empty. It will be cleaned up if no views are registered with it.
        /// </summary>
        bool Empty { get; }

        /// <summary>
        /// When binding is removed it notifies owning collection via this signal.
        /// </summary>
        Signal RemovedSignal { get; }

        /// <summary>
        /// When value is updated, this signal is emitted.
        /// </summary>
        Signal ValueUpdatedSignal { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Register a property belonging to a view with this binding.
        /// </summary>
        /// <param name="view">View instance owning the property</param>
        /// <param name="property">PropertyInfo of the the property.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>False if view instance was already binded, otherwise true.</returns>
        bool RegisterViewProperty(IView view, PropertyInfo property, out string errorMessage);

        /// <summary>
        /// Unregister view.
        /// </summary>
        /// <param name="view">View instance to unregister.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>False if view instance was not registered, otherwise true.</returns>
        bool UnregisterView(IView view, out string errorMessage);
        #endregion
    }
}