namespace cpGames.core.RapidIoC.Tests;

[TestClass]
public class Rapid_Injection_Tests
{
    #region Methods
    [TestMethod]
    public void Comprehensive_Binding_Test()
    {
        var key1 = Rapid.InvalidKey;

        var bindOutcome =
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, Globals.TEST_STR_VAL_1) &&
            Rapid.Bind<Nested1>(contextKey) &&
            Rapid.Bind<Nested2>(Rapid.RootKey);
        Assert.IsTrue(bindOutcome);

        var view = new TestView();
        view.RegisterWithContext();

        Assert.IsTrue(view.Name.Equals(Globals.TEST_STR_VAL_1));
        Assert.IsNotNull(view.Nested1);
        Assert.IsNotNull(view.Nested2);
        Assert.AreEqual(Rapid.Contexts.Count, 1);

        Assert.IsTrue(Rapid.Bind(key1, contextKey, Globals.TEST_STR_VAL_2));
        Assert.IsTrue(view.Name.Equals(Globals.TEST_STR_VAL_2));

        Assert.IsTrue(Rapid.UnregisterView(view));

        Assert.IsTrue(Rapid.Unbind<Nested2>(Rapid.RootKey));
        Assert.AreEqual(Rapid.Contexts.Root.GetBindingCount(true), 0);

        Assert.IsTrue(Rapid.Unbind<Nested1>(contextKey));
        Assert.IsTrue(Rapid.Unbind(key1, contextKey));
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Binding_Move_Test()
    {
        var view1 = new TestView();
        var view2 = new TestView();
        Assert.IsTrue(
            view2.RegisterWithContext() &&
            view1.RegisterWithContext());

        var key1 = Rapid.InvalidKey;
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, Globals.TEST_STR_VAL_1));

        Assert.IsTrue(view1.Name.Equals(Globals.TEST_STR_VAL_1));
        Assert.IsTrue(view2.Name.Equals(Globals.TEST_STR_VAL_1));
        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            view1.UnregisterFromContext() &&
            view2.UnregisterFromContext());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Binding_Reverse_Move_Test()
    {
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out var key1) &&
            Rapid.Bind(key1, Rapid.RootKey, Globals.TEST_STR_VAL_1));

        var view1 = new TestView();
        Assert.IsTrue(view1.RegisterWithContext());
        Assert.AreEqual(view1.Name, Globals.TEST_STR_VAL_1);
        Assert.IsTrue(Rapid.Unbind(key1, Rapid.RootKey));
        Assert.IsTrue(view1.UnregisterFromContext());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
        Assert.AreEqual(Rapid.Contexts.Root.GetBindingCount(true), 0);
    }

    [TestMethod]
    public void GetBindingValue_Test()
    {
        var key1 = Rapid.InvalidKey;
        var value1 = string.Empty;
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, Globals.TEST_STR_VAL_1) &&
            Rapid.GetBindingValue(key1, contextKey, out value1));

        Assert.AreEqual(value1, Globals.TEST_STR_VAL_1);
        Assert.IsTrue(Rapid.Unbind(key1, contextKey));
        Assert.AreEqual(Rapid.Contexts.Count, 0);

        Assert.IsTrue(Rapid.Bind<Nested1>(Rapid.RootKey));
        Assert.IsTrue(Rapid.GetBindingValue<Nested1>(Rapid.RootKey, out var value2));
        Assert.IsInstanceOfType(value2, typeof(Nested1));
        Assert.IsNotNull(value2);
        Assert.IsTrue(Rapid.Unbind<Nested1>(Rapid.RootKey));
        Assert.AreEqual(Rapid.Contexts.Count, 0);
        Assert.AreEqual(Rapid.Contexts.Root.GetBindingCount(true), 0);
    }
    #endregion
}