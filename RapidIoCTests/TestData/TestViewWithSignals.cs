namespace cpGames.core.RapidIoC.Tests;

public class TestViewWithSignals : View
{
    #region Fields
    public int n = -1;
    public string s = string.Empty;
    #endregion

    #region Properties
    public override IKey ContextKey { get; }

    [Inject] public TestNoParamsSignal TestNoParamsSignal { get; set; } = null!;
    [Inject] public TestOneParamSignal TestOneParamSignal { get; set; } = null!;
    [Inject] public TestTwoParamsSignal TestTwoParamsSignal { get; set; } = null!;
    [Inject] public TestNoParamsOutcomeSignal TestNoParamsOutcomeSignal { get; set; } = null!;
    [Inject] public TestOneParamOutcomeSignal TestOneParamOutcomeSignal { get; set; } = null!;
    [Inject] public TestTwoParamsOutcomeSignal TestTwoParamsOutcomeSignal { get; set; } = null!;
    #endregion

    #region Constructors
    public TestViewWithSignals(IKey contextKey)
    {
        ContextKey = contextKey;
    }
    #endregion

    #region Methods
    public void OnTestNoParams()
    {
        n = 42;
    }

    public void OnTestOneParam(int val)
    {
        n = val;
    }

    public void OnTestTwoParams(int val1, string val2)
    {
        n = val1;
        s = val2;
    }

    public Outcome OnTestNoParamsOutcome()
    {
        n = 42;
        return Outcome.Success();
    }

    public Outcome OnTestOneParamOutcome(int val)
    {
        n = val;
        return Outcome.Success();
    }

    public Outcome OnTestTwoParamsOutcome(int val1, string val2)
    {
        n = val1;
        s = val2;
        return Outcome.Success();
    }
    #endregion
}