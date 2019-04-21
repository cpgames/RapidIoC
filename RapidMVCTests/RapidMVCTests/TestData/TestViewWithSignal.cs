namespace cpGames.core.RapidMVC.Tests
{
    public class TestViewWithSignal : View
    {
        #region Fields
        public int n;
        #endregion

        #region Properties
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        [Inject(Globals.INJECT_KEY1)] public Signal<int> TestSignal { get; set; }
        #endregion

        #region Listeners
        public void OnTest(int val)
        {
            n = val;
        }
        #endregion
    }
}