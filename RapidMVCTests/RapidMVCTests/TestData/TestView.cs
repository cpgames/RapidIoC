namespace cpGames.core.RapidMVC.Tests
{
    public class TestView : View
    {
        #region Properties
        [Inject(Globals.INJECT_KEY1)]
        public string Name { get; set; }

        [Inject]
        public Nested1 Nested1 { get; set; }

        [Inject]
        public Nested2 Nested2 { get; set; }
        
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
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