namespace cpGames.core.RapidMVC.Tests
{
    [Context("TestContext")]
    public class TestView : IView
    {
        #region Properties
        [Inject("TestName")]
        public string Name { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}