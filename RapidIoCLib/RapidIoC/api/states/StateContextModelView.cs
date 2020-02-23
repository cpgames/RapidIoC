namespace cpGames.core.RapidIoC
{
    public class ContextView<T_MODEL, T_STATE> :
        View<T_MODEL>,
        IContext<T_STATE> where T_STATE : IStateBase
    {
        #region Fields
        private readonly Signal _stateChangedSignal = new Signal();
        #endregion

        #region Properties
        public Signal StateChangedSignal => _stateChangedSignal;
        #endregion

        #region IContext<T_STATE> Members
        public T_STATE State { get; set; }

        public void SetState<U>() where U : T_STATE
        {
            if (State != null)
            {
                DisposeState();
            }
            State = StateContextMethods.SetState<U>(this);
            InitializeState();
            StateChangedSignal.Dispatch();
            FinalizeStateChange();
        }
        #endregion

        #region Methods
        public virtual void InitializeState()
        {
            State.Start();
        }

        public virtual void FinalizeStateChange() { }

        public virtual void DisposeState()
        {
            State.Stop();
        }
        #endregion
    }

    public class StateView<T_MODEL, T_CONTEXT> :
        View<T_MODEL>, IState<T_CONTEXT>
        where T_CONTEXT : IContextBase
    {
        #region Fields
        private T_CONTEXT _context;
        #endregion

        #region IState<T_CONTEXT> Members
        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void SetContext(IContextBase context)
        {
            _context = (T_CONTEXT)context;
        }

        public T_CONTEXT Context => _context;
        #endregion
    }

    public class StateView<T_CONTEXT> : IState<T_CONTEXT>
        where T_CONTEXT : IContextBase
    {
        #region Fields
        private T_CONTEXT _context;
        #endregion

        #region IState<T_CONTEXT> Members
        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void SetContext(IContextBase context)
        {
            _context = (T_CONTEXT)context;
        }

        public T_CONTEXT Context => _context;
        #endregion
    }

    public class StateContextView<T_MODEL, T_STATE, T_CONTEXT> :
        ContextView<T_MODEL, T_STATE>, IState<T_CONTEXT>
        where T_STATE : IStateBase
        where T_CONTEXT : IContextBase
    {
        #region Fields
        private T_CONTEXT _context;
        #endregion

        #region IState<T_CONTEXT> Members
        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void SetContext(IContextBase context)
        {
            _context = (T_CONTEXT)context;
        }

        public T_CONTEXT Context => _context;
        #endregion
    }
}