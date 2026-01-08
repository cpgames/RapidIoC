using System;

namespace cpGames.core.RapidIoC
{
    public abstract class BaseCommandView : View, IBaseCommand
    {
        #region Fields
        private bool _executing;
        internal bool _retain;
        protected internal readonly object _syncRoot = new();
        #endregion

        #region Properties
        public virtual bool AllowMultipleExecutions => false;
        public virtual bool EndOnRelease => false;
        #endregion

        #region IBaseCommand Members
        public virtual Outcome Connect()
        {
            return RegisterWithContext();
        }

        public virtual Outcome Release()
        {
            if (_executing)
            {
                if (EndOnRelease)
                {
                    EndExecute();
                }
                else
                {
                    return Outcome.Fail($"Command <{this}> is still executing. Call EndExecute first.");
                }
            }
            return UnregisterFromContext();
        }
        #endregion

        #region Methods
        protected void BeginExecute()
        {
            if (_executing && !AllowMultipleExecutions)
            {
                throw new Exception($"Command <{this}> is already executing.");
            }
            if (!_executing)
            {
                _executing = true;
                var registerWithContextOutcome = RegisterWithContext();
                if (!registerWithContextOutcome)
                {
                    throw new Exception(registerWithContextOutcome.ErrorMessage);
                }
            }
        }

        protected void EndExecute()
        {
            if (!_executing)
            {
                throw new Exception($"Command <{this}> is not executing.");
            }
            _executing = false;
            var unregisterWithContextOutcome = UnregisterFromContext();
            if (!unregisterWithContextOutcome)
            {
                throw new Exception(unregisterWithContextOutcome.ErrorMessage);
            }
        }

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