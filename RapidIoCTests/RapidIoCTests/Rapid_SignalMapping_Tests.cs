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
            Assert.AreEqual(signal.CommandCount, 1);
            signal.Dispatch(5);
            Assert.AreEqual(view.n, 5);
            Rapid.Unbind(Globals.INJECT_KEY1);
            view.UnregisterFromContext();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void Dynamic_Binding2_Test()
        {
            var view = new TestViewWithSignal();
            view.RegisterWithContext();
            
            var signal1 = new TestSignalB();
            Rapid.Bind(Globals.INJECT_KEY1, signal1);
            Assert.AreEqual(signal1.CommandCount, 1);

            var signal2 = new TestSignalB();
            Rapid.Bind(Globals.INJECT_KEY2, signal2);
            Assert.AreEqual(signal2.CommandCount, 0);

            view.UnregisterFromContext();
            Assert.AreEqual(signal1.CommandCount, 0);
            Assert.AreEqual(signal2.CommandCount, 0);

            Rapid.Unbind(Globals.INJECT_KEY1);
            Rapid.Unbind(Globals.INJECT_KEY2);

            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }
        #endregion
    }
}