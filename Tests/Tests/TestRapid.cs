using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidMVC.Tests
{
    [TestClass]
    public class TestRapid
    {
        #region Methods
        [TestMethod]
        public void BindingTest()
        {
            var testContext = "TestContext";
            var testValue1 = "I am a test";
            var testValue2 = "I am updated test";

            Rapid.Bind("TestName", testValue1, testContext);
            Rapid.Bind<Nested1>(testContext);
            Rapid.Bind<Nested2>();

            var view = new TestView();

            Assert.IsTrue(view.Name.Equals(testValue1));
            Assert.IsNotNull(view.Nested1);
            Assert.IsNotNull(view.Nested2);
            Assert.AreEqual(Rapid.Contexts.Count, 1);
            Assert.IsFalse(view.PropertyUpdated);

            Rapid.Bind("TestName", testValue2, testContext);
            Assert.IsTrue(view.PropertyUpdated);
            Assert.IsTrue(view.Name.Equals(testValue2));

            Rapid.UnregisterView(view);
            Assert.AreEqual(Rapid.Contexts.Count, 0);

            Rapid.Unbind<Nested2>();
            Assert.AreEqual(Rapid.Contexts.Root.Bindings.Count, 0);
        }
        #endregion
    }
}