namespace cpGames.core.RapidMVC.Tests
{
    [Context("TestContext")]
    public class TestViewWithSignal : View
    {
        #region Fields
        public int n;
        #endregion

        #region Properties
        [Inject("TestSignal")] public Signal<int> TestSignal { get; set; }
        #endregion

        #region Listeners
        public void OnTest(int val)
        {
            n = val;
        }
        #endregion
    }
}