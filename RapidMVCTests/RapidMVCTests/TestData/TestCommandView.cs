namespace cpGames.core.RapidMVC.Tests
{
    public class CommandData
    {
        #region Fields
        public int n;
        public string t = string.Empty;
        #endregion
    }

    public class TestCommandViewA : CommandView
    {
        #region Properties
        [Inject(Globals.INJECT_KEY1)]
        public CommandData CommandData { get; set; }

        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Execute()
        {
            CommandData.n++;
        }
        #endregion
    }

    public class TestCommandViewB : CommandView<int>
    {
        #region Properties
        [Inject(Globals.INJECT_KEY1)]
        public CommandData CommandData { get; set; }
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Execute(int value)
        {
            CommandData.n += value;
        }
        #endregion
    }

    public class TestCommandViewC : CommandView<int, string>
    {
        #region Properties
        [Inject(Globals.INJECT_KEY1)]
        public CommandData CommandData { get; set; }
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Execute(int value, string text)
        {
            CommandData.n += value;
            CommandData.t = text;
        }
        #endregion
    }
}