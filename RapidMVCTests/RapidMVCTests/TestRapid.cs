using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidMVC.Tests
{
    [TestClass]
    public class TestRapid
    {
        #region Methods
        [TestMethod]
        public void ComprehensiveBindingTest()
        {
            Rapid.Bind("TestName", Globals.TEST_STR_VAL_1, Globals.TEST_CONTEXT_NAME);
            Rapid.Bind<Nested1>(Globals.TEST_CONTEXT_NAME);
            Rapid.Bind<Nested2>();

            var view = new TestView();

            Assert.IsTrue(view.Name.Equals(Globals.TEST_STR_VAL_1));
            Assert.IsNotNull(view.Nested1);
            Assert.IsNotNull(view.Nested2);
            Assert.AreEqual(Rapid.Contexts.Count, 1);
            Assert.IsFalse(view.PropertyUpdated);

            Rapid.Bind("TestName", Globals.TEST_STR_VAL_2, Globals.TEST_CONTEXT_NAME);
            Assert.IsTrue(view.PropertyUpdated);
            Assert.IsTrue(view.Name.Equals(Globals.TEST_STR_VAL_2));

            Rapid.UnregisterView(view);

            Rapid.Unbind<Nested2>();
            Assert.AreEqual(Rapid.Contexts.Root.BindingCount, 0);

            Rapid.Unbind<Nested1>(Globals.TEST_CONTEXT_NAME);
            Rapid.Unbind("TestName", Globals.TEST_CONTEXT_NAME);
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void BindingMoveTest()
        {
            // inject views with local binding of "TestName"
            var view1 = new TestView();
            var view2 = new TestView();
            // inject global binding for "TestName", all local bindings should be moved to Root context
            Rapid.Bind("TestName", Globals.TEST_STR_VAL_1);
            Assert.IsTrue(view1.Name.Equals(Globals.TEST_STR_VAL_1));
            Assert.IsTrue(view2.Name.Equals(Globals.TEST_STR_VAL_1));
            Rapid.Unbind("TestName");
            view1.UnregisterFromContext();
            view2.UnregisterFromContext();
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
        public void TestCommandViewA()
        {
            var signal = new TestSignalA();
            var command = new TestCommandViewA();
            signal.AddCommand(command);
            signal.Dispatch();
            Assert.AreEqual(command._value, 1);
            Assert.IsTrue(string.IsNullOrEmpty(command.InjectedText));
            var text = "test";
            Rapid.Bind("InjectedText", text, Globals.TEST_CONTEXT_NAME);
            Assert.AreEqual(command.InjectedText, text);
            Rapid.Unbind("InjectedText", Globals.TEST_CONTEXT_NAME);
            Assert.IsTrue(signal.RemoveCommand(command));
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void TestCommandViewB()
        {
            var signal = new TestSignalA();
            var command = (TestCommandViewA)signal.AddCommand<TestCommandViewA>();
            signal.Dispatch();
            Assert.AreEqual(command._value, 1);
            Assert.IsTrue(string.IsNullOrEmpty(command.InjectedText));
            var text = "test";
            Rapid.Bind("InjectedText", text, Globals.TEST_CONTEXT_NAME);
            Assert.AreEqual(command.InjectedText, text);
            Rapid.Unbind("InjectedText", Globals.TEST_CONTEXT_NAME);
            signal.RemoveCommand(command);
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void TestSignalMapping()
        {
            var signal = new Signal<int>();
            Rapid.Bind("TestSignal", signal, Globals.TEST_CONTEXT_NAME);
            var view = new TestViewWithSignal();
            Assert.AreEqual(view.n, 0);
            signal.Dispatch(5);
            Assert.AreEqual(view.n, 5);
            Rapid.Unbind("TestSignal", Globals.TEST_CONTEXT_NAME);
            view.UnregisterFromContext();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void TestSignalMappingDynamic()
        {
            var view = new TestViewWithSignal();
            Assert.AreEqual(view.n, 0);
            var signal = new TestSignalB();
            Rapid.Bind("TestSignal", signal);
            signal.Dispatch(5);
            Assert.AreEqual(view.n, 5);
            Rapid.Unbind("TestSignal", Globals.TEST_CONTEXT_NAME);
            view.UnregisterFromContext();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }
        #endregion
    }
}