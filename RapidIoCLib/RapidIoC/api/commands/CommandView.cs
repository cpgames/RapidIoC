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
                throw new Exception(string.Format("Command <{0}> is still executing. Call EndExecute first.", this));
            }
        }
        #endregion

        #region Methods
        protected void BeginExecute()
        {
            if (_executing)
            {
                throw new Exception(string.Format("Command <{0}> is already executing.", this));
            }
            _executing = true;
            RegisterWithContext();
        }

        protected void EndExecute()
        {
            if (!_executing)
            {
                throw new Exception(string.Format("Command <{0}> is not executing.", this));
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

    public abstract class CommandView : BaseCommandView, ICommand
    {
        #region ICommand Members
        public void Execute()
        {
            lock (_syncRoot)
            {
                BeginExecute();
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion
    }

    public abstract class CommandView<TModel> : BaseCommandView, ICommand<TModel>
    {
        #region Properties
        public TModel Model { get; private set; }
        #endregion

        #region ICommand<TModel> Members
        public void Execute(TModel model)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                Model = model;
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion
    }

    public abstract class CommandView<TModel1, TModel2> : BaseCommandView, ICommand<TModel1, TModel2>
    {
        #region Properties
        public TModel1 Model1 { get; private set; }
        public TModel2 Model2 { get; private set; }
        #endregion

        #region ICommand<TModel1,TModel2> Members
        public void Execute(TModel1 model1, TModel2 model2)
        {
            lock (_syncRoot)
            {
                BeginExecute();
                Model1 = model1;
                Model2 = model2;
                ExecuteInternal();
                if (!_retain)
                {
                    EndExecute();
                }
            }
        }
        #endregion
    }
}