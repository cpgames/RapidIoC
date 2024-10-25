namespace cpGames.core.RapidIoC.Tests;

[TestClass]
public class Rapid_SignalMapping_Tests
{
    #region Methods
    [TestMethod]
    public void Test_No_Parameter()
    {
        var signal = new TestNoParamsSignal();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.Bind<TestNoParamsSignal>(contextKey, signal));

        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        signal.Dispatch();
        Assert.AreEqual(view.n, 42);
        Assert.IsTrue(
            Rapid.Unbind<TestNoParamsSignal>(contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Test_One_Parameter()
    {
        var signal = new TestOneParamSignal();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.Bind<TestOneParamSignal>(contextKey, signal));

        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        signal.Dispatch(5);
        Assert.AreEqual(view.n, 5);
        Assert.IsTrue(
            Rapid.Unbind<TestOneParamSignal>(contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Test_Two_Parameters()
    {
        var signal = new TestTwoParamsSignal();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.Bind<TestTwoParamsSignal>(contextKey, signal));

        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        signal.Dispatch(5, "hello");
        Assert.AreEqual(view.n, 5);
        Assert.AreEqual(view.s, "hello");
        Assert.IsTrue(
            Rapid.Unbind<TestTwoParamsSignal>(contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Test_No_Parameter_Outcome()
    {
        var signal = new TestNoParamsOutcomeSignal();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.Bind<TestNoParamsOutcomeSignal>(contextKey, signal));

        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        var result = signal.DispatchResult();
        Assert.IsTrue(result);
        Assert.AreEqual(view.n, 42);
        Assert.IsTrue(
            Rapid.Unbind<TestNoParamsOutcomeSignal>(contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Test_One_Parameter_Outcome()
    {
        var signal = new TestOneParamOutcomeSignal();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.Bind<TestOneParamOutcomeSignal>(contextKey, signal));

        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        var result = signal.DispatchResult(5);
        Assert.IsTrue(result);
        Assert.AreEqual(view.n, 5);
        Assert.IsTrue(
            Rapid.Unbind<TestOneParamOutcomeSignal>(contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Test_Two_Parameters_Outcome()
    {
        var signal = new TestTwoParamsOutcomeSignal();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.Bind<TestTwoParamsOutcomeSignal>(contextKey, signal));

        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        Assert.AreEqual(signal.CommandCount, 1);
        var result = signal.DispatchResult(5, "hello");
        Assert.IsTrue(result);
        Assert.AreEqual(view.n, 5);
        Assert.AreEqual(view.s, "hello");
        Assert.IsTrue(
            Rapid.Unbind<TestTwoParamsOutcomeSignal>(contextKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(signal.CommandCount, 0);
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Dynamic_Binding_Test()
    {
        Assert.IsTrue(Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey));
        var view = new TestViewWithSignals(contextKey);
        Assert.IsTrue(view.RegisterWithContext());
        var signal = new TestOneParamSignal();

        Assert.IsTrue(
            Rapid.Bind<TestOneParamSignal>(Rapid.RootKey, signal));

        signal.Dispatch(5);
        Assert.AreEqual(view.n, 5);
        Assert.IsTrue(
            Rapid.Unbind<TestOneParamSignal>(Rapid.RootKey) &&
            view.UnregisterFromContext());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }
    #endregion
}