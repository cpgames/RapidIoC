namespace cpGames.core.RapidIoC
{
    public class ContextView<T_Model, TState> :
        View<T_Model>,
        IContext<TState> where TState : IStateBase
    {
        #region Properties
        public ISignal StateChangedSignal { get; } = new Signal();
        #endregion

        #region IContext<TState> Members
        public TState? State { get; set; }

        public void SetState<UState>() where UState : TState
        {
            if (State != null)
            {
                DisposeState();
            }
            State = StateContextMethods.SetState<UState>(this);
            InitializeState();
            StateChangedSignal.Dispatch();
            FinalizeStateChange();
        }
        #endregion

        #region Methods
        public virtual void InitializeState()
        {
            State?.Start();
        }

        public virtual void FinalizeStateChange() { }

        public virtual void DisposeState()
        {
            State?.Stop();
        }
        #endregion
    }

    public class StateView<T_Model, TContext> :
        View<T_Model>, IState<TContext>
        where TContext : IContextBase
    {
        #region Fields
        private TContext? _context;
        #endregion

        #region IState<TContext> Members
        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void SetContext(IContextBase context)
        {
            _context = (TContext)context;
        }

        public TContext? Context => _context;
        #endregion
    }

    public class StateView<TContext> : IState<TContext>
        where TContext : IContextBase
    {
        #region Fields
        private TContext? _context;
        #endregion

        #region IState<TContext> Members
        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void SetContext(IContextBase context)
        {
            _context = (TContext)context;
        }

        public TContext? Context => _context;
        #endregion
    }

    public class StateContextView<TModel, TState, TContext> :
        ContextView<TModel, TState>, IState<TContext>
        where TState : IStateBase
        where TContext : IContextBase
    {
        #region Fields
        private TContext? _context;
        #endregion

        #region IState<TContext> Members
        public virtual void Start() { }

        public virtual void Stop() { }

        public virtual void SetContext(IContextBase context)
        {
            _context = (TContext)context;
        }

        public TContext? Context => _context;
        #endregion
    }
}