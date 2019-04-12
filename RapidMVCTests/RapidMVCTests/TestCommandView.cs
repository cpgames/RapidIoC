namespace cpGames.core.RapidMVC.Tests
{
    public class TestCommandViewA : CommandView
    {
        #region Fields
        public int _value;
        #endregion

        #region Properties
        [Inject("InjectedText")]
        public string InjectedText { get; set; }

        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Execute()
        {
            _value++;
        }
        #endregion
    }

    public class TestCommandViewB : CommandView<int>
    {
        #region Fields
        public int _value;
        #endregion

        #region Properties
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Execute(int value)
        {
            _value += value;
        }
        #endregion
    }

    public class TestCommandViewC : CommandView<int, string>
    {
        #region Fields
        public int _value;
        public string _text = "";
        #endregion

        #region Properties
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Execute(int value, string text)
        {
            _value += value;
            _text = text;
        }
        #endregion
    }
}