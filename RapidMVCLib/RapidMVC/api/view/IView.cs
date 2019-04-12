using System.Collections.Generic;
using cpGames.core.RapidMVC.impl;

namespace cpGames.core.RapidMVC
{
    /// <summary>
    /// Views are registered with Contexts.
    /// You can either derive from default View or implement your own
    /// (particularly when your view has its own base class that is not a view).
    /// </summary>
    public interface IView
    {
        #region Properties
        /// <summary>
        /// A property belonging to this view with specific key was updated.
        /// </summary>
        Signal<IBindingKey> PropertyUpdatedSignal { get; }

        /// <summary>
        /// Automatically mapped signals to listeners. Used for internal use.
        /// </summary>
        List<ISignalMapping> SignalMappings { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Unregister view from context, call this whenever view is destroyed.
        /// </summary>
        void UnregisterFromContext();
        #endregion
    }
}