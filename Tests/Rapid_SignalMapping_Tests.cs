namespace cpGames.core.RapidIoC.Tests;

[TestClass]
public class Rapid_SignalMapping_Tests
{
    #region Methods
    [TestMethod]
    public void Basic_Test()
    {
        var signal = new Signal<int>();
        var key1 = Rapid.InvalidKey;
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, signal));

        var view = new TestViewWithSignal();
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        signal.Dispatch(5);
        Assert.AreEqual(view.n, 5);
        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Dynamic_Binding_Test()
    {
        var view = new TestViewWithSignal();
        Assert.IsTrue(view.RegisterWithContext());
        var signal = new TestSignalB();

        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out var key1) &&
            Rapid.Bind(key1, Rapid.RootKey, signal));

        signal.Dispatch(5);
        Assert.AreEqual(view.n, 5);
        Assert.IsTrue(
            Rapid.Unbind(key1, Rapid.RootKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }
    #endregion
}