namespace cpGames.core.RapidMVC
{
    /// <summary>
    /// Views are registered with Contexts.
    /// You can either derive from default View or implement your own.
    /// </summary>
    public interface IView
    {
        #region Properties
        Signal<IBindingKey> PropertyUpdatedSignal { get; }
        #endregion
    }
}