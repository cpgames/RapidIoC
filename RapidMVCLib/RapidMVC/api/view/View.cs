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
        #region IView Members
        public abstract string ContextName { get; }
        public List<ISignalMapping> SignalMappings { get; } = new List<ISignalMapping>();

        public void RegisterWithContext()
        {
            Rapid.RegisterView(this);
        }

        public void UnregisterFromContext()
        {
            Rapid.UnregisterView(this);
        }
        #endregion
    }
}