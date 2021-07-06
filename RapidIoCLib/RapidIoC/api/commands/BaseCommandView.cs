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
        public virtual void Connect() { }

        public virtual void Release()
        {
            if (_executing)
            {
                throw new Exception($"Command <{this}> is still executing. Call EndExecute first.");
            }
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
            RegisterWithContext();
        }

        protected void EndExecute()
        {
            if (!_executing)
            {
                throw new Exception($"Command <{this}> is not executing.");
            }
            _executing = false;
            UnregisterFromContext();
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