namespace cpGames.core.RapidMVC.Tests
{
    [Context("TestContext")]
    public class TestView : IView
    {
        #region Properties
        [Inject("TestName")]
        public string Name { get; set; }

        [Inject]
        public Nested1 Nested1 { get; set; }

        [Inject]
        public Nested2 Nested2 { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }

    public class Nested1 { }

    public class Nested2 { }
}