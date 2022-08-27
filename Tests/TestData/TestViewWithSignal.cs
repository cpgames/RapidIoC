namespace cpGames.core.RapidIoC.Tests;

public class TestViewWithSignal : View
{
    #region Fields
    private IKey _contextKey = Rapid.InvalidKey;
    public int n;
    #endregion

    #region Properties
    public override IKey ContextKey => _contextKey;
    [Inject(Globals.INJECT_KEY1)] public Signal<int>? TestSignal { get; set; }
    #endregion

    #region Methods
    protected override Outcome RegisterWithContextInternal()
    {
        return Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out _contextKey);
    }

    public void OnTest(int val)
    {
        n = val;
    }
    #endregion
}