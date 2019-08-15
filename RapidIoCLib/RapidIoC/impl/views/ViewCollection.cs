using System.Collections.Generic;

namespace cpGames.core.RapidIoC.impl
{
    internal class ViewCollection : IViewCollection
    {
        #region Fields
        private readonly List<IView> _views = new List<IView>();
        #endregion

        #region IViewCollection Members
        public int ViewCount => _views.Count;

        public bool RegisterView(IView view, out string errorMessage)
        {
            if (_views.Contains(view))
            {
                errorMessage = string.Format("View <{0}> is already registered.", view);
                return false;
            }
            _views.Add(view);
            errorMessage = string.Empty;
            return true;
        }

        public bool UnregisterView(IView view, out string errorMessage)
        {
            if (!_views.Contains(view))
            {
                errorMessage = string.Format("View <{0}> is not registered.", view);
                return false;
            }
            _views.Remove(view);
            errorMessage = string.Empty;
            return true;
        }

        public bool ClearViews(out string errorMessage)
        {
            while (_views.Count > 0)
            {
                if (!UnregisterView(_views[0], out errorMessage))
                {
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }
        #endregion
    }
}