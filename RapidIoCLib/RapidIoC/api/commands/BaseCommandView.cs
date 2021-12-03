using System;

namespace cpGames.core.RapidIoC
{
    public abstract class BaseCommandView : View, IBaseCommand
    {
        #region Fields
        private bool _executing;
        internal bool _retain;
        protected internal readonly object _syncRoot = new object();
        #endregion

        #region IBaseCommand Members
        public virtual bool Connect(out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }

        public virtual bool Release(out string errorMessage)
        {
            if (_executing)
            {
                errorMessage = $"Command <{this}> is still executing. Call EndExecute first.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
        #endregion

        #region Methods
        protected void BeginExecute()
        {
            if (_executing)
            {
                throw new Exception($"Command <{this}> is already executing.");
            }
            _executing = true;
            if (!RegisterWithContext(out var errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        protected void EndExecute()
        {
            if (!_executing)
            {
                throw new Exception($"Command <{this}> is not executing.");
            }
            _executing = false;
            if (!UnregisterFromContext(out var errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        protected abstract void ExecuteInternal();

        protected void Retain()
        {
            lock (_syncRoot)
            {
                _retain = true;
            }
        }
        #endregion
    }
}