namespace cpGames.core.RapidMVC.Tests
{
    [Context("TestContext")]
    public class TestCommandViewA : CommandView
    {
        #region Fields
        public int _value;
        #endregion

        [Inject("InjectedText")]
        public string InjectedText { get; set; }

        #region Methods
        public override void Execute()
        {
            _value++;
        }
        #endregion
    }

    [Context("TestContext")]
    public class TestCommandViewB : CommandView<int>
    {
        #region Fields
        public int _value;
        #endregion

        #region Methods
        public override void Execute(int value)
        {
            _value += value;
        }
        #endregion
    }

    [Context("TestContext")]
    public class TestCommandViewC : CommandView<int, string>
    {
        #region Fields
        public int _value;
        public string _text = "";
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