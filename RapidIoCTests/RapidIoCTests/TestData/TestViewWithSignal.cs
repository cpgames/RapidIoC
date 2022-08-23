namespace cpGames.core.RapidIoC.Tests
{
    public class TestViewWithSignal : View
    {
        #region Fields
        public int n;
        #endregion

        #region Properties
        public override string ContextName => Globals.TEST_CONTEXT_NAME;
        [Inject(Globals.INJECT_KEY1)] public Signal<int> Test1Signal { get; set; }

        [Inject(Globals.INJECT_KEY2)] public Signal<int> Test2Signal { get; set; }
        #endregion

        #region Listeners
        public void OnTest1(int val)
        {
            n = val;
        }
        #endregion
    }
}