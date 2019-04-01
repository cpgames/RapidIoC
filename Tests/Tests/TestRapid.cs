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
            GlobalContext.Instance.CreateContext("TestContext");
            var context = GlobalContext.Instance.FindContext("TestContext");
            var testValue = "I am a test";
            // bind by name
            context.CreateBinding(new NameBindingKey("TestName"), new Binding(testValue));
            // bind by type with default instantiator
            context.CreateBindingSingleton<Nested1>();
            GlobalContext.Instance.CreateBindingSingleton<Nested2>();

            var view = new TestView();
            view.RegisterWithContext();
            Assert.IsTrue(view.Name.Equals(testValue));
            Assert.IsNotNull(view.Nested1);
            Assert.IsNotNull(view.Nested2);
        }
        #endregion
    }
}