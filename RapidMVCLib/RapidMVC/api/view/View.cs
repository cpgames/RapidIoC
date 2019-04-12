using System.Collections.Generic;
using cpGames.core.RapidMVC.impl;

namespace cpGames.core.RapidMVC
{
    /// <inheritdoc cref="IView" />
    /// <summary>
    /// Default View
    /// </summary>
    public abstract class View : IView
    {
        #region Constructors
        protected View()
        {
            Rapid.RegisterView(this);
        }
        #endregion

        #region IView Members
        public abstract string ContextName { get; }
        public Signal<IBindingKey> PropertyUpdatedSignal { get; } = new Signal<IBindingKey>();
        public List<ISignalMapping> SignalMappings { get; } = new List<ISignalMapping>();

        public void UnregisterFromContext()
        {
            Rapid.UnregisterView(this);
        }
        #endregion
    }
}