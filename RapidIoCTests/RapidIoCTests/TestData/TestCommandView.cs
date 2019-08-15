namespace cpGames.core.RapidIoC.Tests
{
    public class CommandData
    {
        #region Fields
        public int n;
        public string t = string.Empty;
        public bool set;
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

    public class TestCommandViewD : CommandView<bool>
    {
        #region Properties
        [Inject(Globals.INJECT_KEY1)]
        public CommandData CommandData { get; set; }
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        #endregion

        #region Methods
        public override void Connect()
        {
            base.Connect();

            if (CommandData.n > 0)
            {
                Execute(true);
            }
        }

        public override void Execute(bool value)
        {
            CommandData.set = value;
        }
        #endregion
    }
}