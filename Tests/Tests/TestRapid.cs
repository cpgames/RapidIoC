using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidMVC.Tests
{
    [TestClass]
    public class TestRapid
    {
        #region Methods
        [TestMethod]
        public void NameBindingTest()
        {
            GlobalContext.Instance.CreateContext("TestContext");
            var context = GlobalContext.Instance.FindContext("TestContext");
            var testValue = "I am a test";
            context.CreateBinding(new NameBindingKey("TestName"), new Binding(testValue));
            var view = new TestView();
            view.RegisterWithContext();
            Assert.IsTrue(view.Name.Equals(testValue));
        }
        #endregion
    }
}