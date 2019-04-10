using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidMVC.Tests
{
    [TestClass]
    public class TestRapid
    {
        #region Fields
        private const string testContextName = "TestContext";
        #endregion

        #region Methods
        [TestMethod]
        public void ComprehensiveBindingTest()
        {
            var testValue1 = "I am a test";
            var testValue2 = "I am updated test";

            Rapid.Bind("TestName", testValue1, testContextName);
            Rapid.Bind<Nested1>(testContextName);
            Rapid.Bind<Nested2>();

            var view = new TestView();

            Assert.IsTrue(view.Name.Equals(testValue1));
            Assert.IsNotNull(view.Nested1);
            Assert.IsNotNull(view.Nested2);
            Assert.AreEqual(Rapid.Contexts.Count, 1);
            Assert.IsFalse(view.PropertyUpdated);

            Rapid.Bind("TestName", testValue2, testContextName);
            Assert.IsTrue(view.PropertyUpdated);
            Assert.IsTrue(view.Name.Equals(testValue2));

            Rapid.UnregisterView(view);

            Rapid.Unbind<Nested2>();
            Assert.AreEqual(Rapid.Contexts.Root.BindingCount, 0);

            Rapid.Unbind<Nested1>(testContextName);
            Rapid.Unbind("TestName", testContextName);
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void SignalTest1()
        {
            var n = 0;
            var signal = new TestSignalA();
            signal.AddCommand(() =>
            {
                n = 5;
            }, true);
            signal.Dispatch();
            Assert.AreEqual(n, 5);
        }

        [TestMethod]
        public void SignalTest2()
        {
            var n = 0;
            var signal = new TestSignalB();
            signal.AddCommand(val =>
            {
                n = val;
            }, true);
            signal.Dispatch(5);
            Assert.AreEqual(n, 5);
        }

        [TestMethod]
        public void SignalTest3()
        {
            var n = 0;
            var text = "";
            var signal = new TestSignalC();
            signal.AddCommand((intVal, textVal) =>
            {
                n = intVal;
                text = textVal;
            }, true);
            signal.Dispatch(5, "test");

            Assert.AreEqual(n, 5);
            Assert.AreEqual(text, "test");
        }

        [TestMethod]
        public void TestCommandView()
        {
            var signal = new TestSignalA();
            var command = new TestCommandViewA();
            signal.AddCommand(command);
            signal.Dispatch();
            Assert.AreEqual(command._value, 1);
            Assert.IsTrue(string.IsNullOrEmpty(command.InjectedText));
            var text = "test";
            Rapid.Bind("InjectedText", text, testContextName);
            Assert.AreEqual(command.InjectedText, text);
        }
        #endregion
    }
}