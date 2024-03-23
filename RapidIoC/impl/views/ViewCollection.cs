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

        public Outcome RegisterView(IView view)
        {
            if (_views.Contains(view))
            {
                return Outcome.Fail($"View <{view}> is already registered.", this);
            }
            _views.Add(view);
            return Outcome.Success();
        }

        public Outcome UnregisterView(IView view)
        {
            if (!_views.Contains(view))
            {
                return Outcome.Fail($"View <{view}> is not registered.", this);
            }
            _views.Remove(view);
            return Outcome.Success();
        }

        public Outcome ClearViews()
        {
            while (_views.Count > 0)
            {
                var unregisterViewOutcome = UnregisterView(_views[0]);
                if (!unregisterViewOutcome)
                {
                    return unregisterViewOutcome;
                }
            }
            return Outcome.Success();
        }
        #endregion
    }
}