namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     Default View
    /// </summary>
    public abstract class View : IView
    {
        #region Constructors
        protected View()
        {
            Rapid.RegisterView(this);
        }
        #endregion

        #region IView Members
        public Signal<IBindingKey> PropertyUpdatedSignal { get; } = new Signal<IBindingKey>();
        #endregion
    }
}