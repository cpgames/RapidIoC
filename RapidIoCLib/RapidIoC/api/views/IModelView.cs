namespace cpGames.core.RapidIoC
{
    public interface IModelView : IView
    {
        #region Properties
        bool HasModel { get; }
        Signal ModelSetSignal { get; }
        #endregion
    }
}