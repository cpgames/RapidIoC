namespace cpGames.core.RapidMVC
{
    public interface IView
    {
        #region Properties
        Signal<IBindingKey> PropertyUpdatedSignal { get; }
        #endregion
    }

    public class View : IView
    {
        #region Constructors
        public View()
        {
            Rapid.RegisterView(this);
        }
        #endregion

        #region IView Members
        public Signal<IBindingKey> PropertyUpdatedSignal { get; } = new Signal<IBindingKey>();
        #endregion
    }
}