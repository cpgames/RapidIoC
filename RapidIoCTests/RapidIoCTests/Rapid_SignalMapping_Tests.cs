using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidIoC.Tests
{
    [TestClass]
    public class Rapid_SignalMapping_Tests
    {
        #region Methods
        [TestMethod]
        public void Basic_Test()
        {
            var signal = new Signal<int>();
            Rapid.Bind(Globals.INJECT_KEY1, signal, Globals.TEST_CONTEXT_NAME);
            var view = new TestViewWithSignal();
            view.RegisterWithContext();
            Assert.AreEqual(signal.CommandCount, 1);
            signal.Dispatch(5);
            Assert.AreEqual(view.n, 5);
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            view.UnregisterFromContext();
            Assert.AreEqual(signal.CommandCount, 0);
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void Dynamic_Binding_Test()
        {
            var view = new TestViewWithSignal();
            view.RegisterWithContext();
            var signal = new TestSignalB();
            Rapid.Bind(Globals.INJECT_KEY1, signal);
            signal.Dispatch(5);
            Assert.AreEqual(view.n, 5);
            Rapid.Unbind(Globals.INJECT_KEY1);
            view.UnregisterFromContext();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }
        #endregion
    }
}