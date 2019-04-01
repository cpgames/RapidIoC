namespace cpGames.core.RapidMVC
{
    public class LocalContext : Context
    {
        #region Properties
        public override string Name { get; }
        #endregion

        #region Constructors
        public LocalContext(string name)
        {
            Name = name;
        }
        #endregion
    }
}