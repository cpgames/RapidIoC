namespace cpGames.core.RapidIoC.Tests;

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
    #region Fields
    private IKey _contextKey = Rapid.InvalidKey;
    #endregion

    #region Properties
    [Inject(Globals.INJECT_KEY1)]
    public CommandData? CommandData { get; set; }

    public override IKey ContextKey => _contextKey;
    #endregion

    #region Methods
    protected override Outcome RegisterWithContextInternal()
    {
        return Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out _contextKey);
    }

    protected override void ExecuteInternal()
    {
        if (CommandData != null)
        {
            CommandData.n++;
        }
    }
    #endregion
}
public class TestCommandViewB : CommandView<int>
{
    #region Fields
    private IKey _contextKey = Rapid.InvalidKey;
    #endregion

    #region Properties
    [Inject(Globals.INJECT_KEY1)]
    public CommandData? CommandData { get; set; }
    public override IKey ContextKey => _contextKey;
    #endregion

    #region Methods
    protected override Outcome RegisterWithContextInternal()
    {
        return Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out _contextKey);
    }

    protected override void ExecuteInternal(int @in)
    {
        if (CommandData != null)
        {
            CommandData.n += @in;
        }
    }
    #endregion
}
public class TestCommandViewC : CommandView<int, string>
{
    #region Fields
    private IKey _contextKey = Rapid.InvalidKey;
    #endregion

    #region Properties
    [Inject(Globals.INJECT_KEY1)]
    public CommandData? CommandData { get; set; }
    public override IKey ContextKey => _contextKey;
    #endregion

    #region Methods
    protected override Outcome RegisterWithContextInternal()
    {
        return Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out _contextKey);
    }

    protected override void ExecuteInternal(int in1, string in2)
    {
        if (CommandData != null)
        {
            CommandData.n += in1;
            CommandData.t = in2;
        }
    }
    #endregion
}
public class TestCommandViewD : CommandView<bool>
{
    #region Fields
    private IKey _contextKey = Rapid.InvalidKey;
    #endregion

    #region Properties
    [Inject(Globals.INJECT_KEY1)]
    public CommandData? CommandData { get; set; }
    public override IKey ContextKey => _contextKey;
    #endregion

    #region Methods
    protected override Outcome RegisterWithContextInternal()
    {
        return Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out _contextKey);
    }

    public override Outcome Connect()
    {
        base.Connect();

        if (CommandData == null)
        {
            return Outcome.Fail();
        }

        if (CommandData.n > 0)
        {
            Execute(true);
        }
        return Outcome.Success();
    }

    protected override void ExecuteInternal(bool value)
    {
        if (CommandData != null)
        {
            CommandData.set = value;
        }
    }
    #endregion
}