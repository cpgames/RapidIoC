namespace cpGames.core.RapidMVC
{
    public interface IBinding
    {
        #region Properties
        object Value { get; }
        #endregion
    }

    public class Binding : IBinding
    {
        #region Constructors
        public Binding(object value)
        {
            Value = value;
        }
        #endregion

        #region IBinding Members
        public object Value { get; }
        #endregion
    }
}