using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidIoC.Tests
{
    [TestClass]
    public class Rapid_Injection_Tests
    {
        #region Methods
        [TestMethod]
        public void Comprehensive_Binding_Test()
        {
            Rapid.Bind(Globals.INJECT_KEY1, Globals.TEST_STR_VAL_1, Globals.TEST_CONTEXT_NAME);
            Rapid.Bind<Nested1>(Globals.TEST_CONTEXT_NAME);
            Rapid.Bind<Nested2>();

            var view = new TestView();
            view.RegisterWithContext();

            Assert.IsTrue(view.Name.Equals(Globals.TEST_STR_VAL_1));
            Assert.IsNotNull(view.Nested1);
            Assert.IsNotNull(view.Nested2);
            Assert.AreEqual(Rapid.Contexts.Count, 1);

            Rapid.Bind(Globals.INJECT_KEY1, Globals.TEST_STR_VAL_2, Globals.TEST_CONTEXT_NAME);
            Assert.IsTrue(view.Name.Equals(Globals.TEST_STR_VAL_2));

            Rapid.UnregisterView(view);

            Rapid.Unbind<Nested2>();
            Assert.AreEqual(Rapid.Contexts.Root.BindingCount, 0);

            Rapid.Unbind<Nested1>(Globals.TEST_CONTEXT_NAME);
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void Binding_Move_Test()
        {
            var view1 = new TestView();
            view1.RegisterWithContext();
            var view2 = new TestView();
            view2.RegisterWithContext();
            Rapid.Bind(Globals.INJECT_KEY1, Globals.TEST_STR_VAL_1);
            Assert.IsTrue(view1.Name.Equals(Globals.TEST_STR_VAL_1));
            Assert.IsTrue(view2.Name.Equals(Globals.TEST_STR_VAL_1));
            Rapid.Unbind(Globals.INJECT_KEY1);
            view1.UnregisterFromContext();
            view2.UnregisterFromContext();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void Binding_Reverse_Move_Test()
        {
            Rapid.Bind(Globals.INJECT_KEY1, Globals.TEST_STR_VAL_1);
            var view1 = new TestView();
            view1.RegisterWithContext();
            Assert.IsTrue(view1.Name.Equals(Globals.TEST_STR_VAL_1));
            Rapid.Unbind(Globals.INJECT_KEY1);
            view1.UnregisterFromContext();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void GetBindingValue_Test()
        {
            Rapid.Bind(Globals.INJECT_KEY1, Globals.TEST_STR_VAL_1, Globals.TEST_CONTEXT_NAME);
            var value1 = Rapid.GetBindingValue(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            Assert.AreEqual(value1, Globals.TEST_STR_VAL_1);
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            Assert.AreEqual(Rapid.Contexts.Count, 0);

            Rapid.Bind<Nested1>();
            var value2 = Rapid.GetBindingValue<Nested1>();
            Assert.IsInstanceOfType(value2, typeof(Nested1));
            Assert.IsNotNull(value2);
            Rapid.Unbind<Nested1>();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
            Assert.AreEqual(Rapid.Contexts.Root.BindingCount, 0);
        }
        #endregion
    }
}