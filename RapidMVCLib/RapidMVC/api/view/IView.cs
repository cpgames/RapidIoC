namespace cpGames.core.RapidMVC
{
    public interface IView
    {
        #region Properties
        Signal<IBindingKey> PropertyUpdatedSignal { get; }
        #endregion
    }
}