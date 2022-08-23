﻿using System;

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
        public virtual Outcome Connect()
        {
            return Outcome.Success();
        }

        public virtual Outcome Release()
        {
            return _executing ?
                Outcome.Fail($"Command <{this}> is still executing. Call EndExecute first.") :
                Outcome.Success();
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
            var registerWithContextOutcome = RegisterWithContext();
            if (!registerWithContextOutcome)
            {
                throw new Exception(registerWithContextOutcome.ErrorMessage);
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