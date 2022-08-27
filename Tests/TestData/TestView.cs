namespace cpGames.core.RapidIoC.Tests;

public class TestView : View
{
    #region Fields
    private IKey _contextKey = Rapid.InvalidKey;
    #endregion

    #region Properties
    [Inject(Globals.INJECT_KEY1)]
    public string? Name { get; set; }

    [Inject]
    public Nested1? Nested1 { get; set; }

    [Inject]
    public Nested2? Nested2 { get; set; }

    public override IKey ContextKey => _contextKey;
    #endregion

    #region Methods
    protected override Outcome RegisterWithContextInternal()
    {
        return Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out _contextKey);
    }

    public override string ToString()
    {
        return Name ?? string.Empty;
    }
    #endregion
}
public class Nested1 { }
public class Nested2 { }