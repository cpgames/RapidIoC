namespace cpGames.core.RapidIoC.Tests;

[TestClass]
public class Rapid_Signal_Tests
{
    #region Methods
    [TestMethod]
    public void NoParam_Lambda_Test()
    {
        var n = 0;
        var signal = new TestSignalA();
        signal.AddCommand(() =>
        {
            n++;
        });
        signal.Dispatch();
        Assert.AreEqual(n, 1);
        signal.Dispatch();
        Assert.AreEqual(n, 2);
    }

    [TestMethod]
    public void NoParam_Once_Lambda_Test()
    {
        var n = 0;
        var signal = new TestSignalA();
        signal.AddCommand(() =>
        {
            n++;
        }, keyData: null, once: true);
        signal.Dispatch();
        Assert.AreEqual(n, 1);
        signal.Dispatch();
        Assert.AreEqual(n, 1);
    }

    [TestMethod]
    public void OnceParam_Lambda_Test()
    {
        var n = 0;
        var signal = new TestSignalB();
        signal.AddCommand(val =>
        {
            n = val;
        });
        signal.Dispatch(5);
        Assert.AreEqual(n, 5);
        signal.Dispatch(4);
        Assert.AreEqual(n, 4);
    }

    [TestMethod]
    public void TwoParam_Lambda_Test()
    {
        var n = 0;
        var text = "";
        var signal = new TestSignalC();
        signal.AddCommand((intVal, textVal) =>
        {
            n = intVal;
            text = textVal;
        });
        signal.Dispatch(5, "five");
        Assert.AreEqual(n, 5);
        Assert.AreEqual(text, "five");
        signal.Dispatch(4, "four");
        Assert.AreEqual(n, 4);
        Assert.AreEqual(text, "four");
    }

    [TestMethod]
    public void NoParam_CommandView_Test()
    {
        var key1 = Rapid.InvalidKey;
        var commandData = new CommandData();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, commandData));

        var signal = new TestSignalA();
        Assert.IsTrue(signal.AddCommand<TestCommandViewA>());
        signal.Dispatch();
        Assert.AreEqual(commandData.n, 1);
        signal.Dispatch();
        Assert.AreEqual(commandData.n, 2);
        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            signal.ClearCommands());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void OneParam_CommandView_Test()
    {
        var key1 = Rapid.InvalidKey;
        var commandData = new CommandData();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, commandData));

        var signal = new TestSignalB();
        Assert.IsTrue(signal.AddCommand<TestCommandViewB>());
        signal.Dispatch(5);
        Assert.AreEqual(commandData.n, 5);
        signal.Dispatch(4);
        Assert.AreEqual(commandData.n, 9);
        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            signal.ClearCommands());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void TwoParam_CommandView_Test()
    {
        var key1 = Rapid.InvalidKey;
        var commandData = new CommandData();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, commandData));

        var signal = new TestSignalC();
        Assert.IsTrue(signal.AddCommand<TestCommandViewC>());
        signal.Dispatch(5, "five");
        Assert.AreEqual(commandData.n, 5);
        Assert.AreEqual(commandData.t, "five");
        signal.Dispatch(4, "four");
        Assert.AreEqual(commandData.n, 9, "four");
        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            signal.ClearCommands());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Duplicate_Command_Test()
    {
        var key1 = Rapid.InvalidKey;
        var commandData = new CommandData();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, commandData));

        var signal = new TestSignalA();
        Assert.IsTrue(signal.AddCommand<TestCommandViewA>());
        Assert.IsFalse(signal.AddCommand<TestCommandViewA>());
        Assert.AreEqual(signal.CommandCount, 1);
        signal.Dispatch();
        Assert.AreEqual(commandData.n, 1);
        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            signal.ClearCommands());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }

    [TestMethod]
    public void Dispatch_While_Modifying_Commands_Test()
    {
        var n = 0;
        var signal = new TestSignalA();
        Assert.IsTrue(signal.AddCommand(() =>
        {
            signal.AddCommand(() =>
            {
                n = 5;
            });
        }));
        signal.Dispatch();
        Assert.AreEqual(n, 0);
        signal.Dispatch();
        Assert.AreEqual(n, 5);
    }

    [TestMethod]
    public void Keyed_Commands_Test()
    {
        var a = 0;
        var b = 0;
        var c = 0;
        var signal = new TestSignalA();
        Assert.IsTrue(
            signal.AddCommand(() =>
            {
                a++;
            }, "a") &&
            signal.AddCommand(() =>
            {
                b++;
            }, "b") &&
            signal.AddCommand(() =>
            {
                c++;
            }, "c")
        );
        Assert.AreEqual(signal.CommandCount, 3);
        signal.Dispatch();
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 1);
        Assert.AreEqual(c, 1);
        Assert.IsTrue(signal.RemoveCommand("a"));
        Assert.AreEqual(signal.CommandCount, 2);
        signal.Dispatch();
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 2);
        Assert.AreEqual(c, 2);
        Assert.IsTrue(signal.RemoveCommand("b"));
        Assert.AreEqual(signal.CommandCount, 1);
        signal.Dispatch();
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 2);
        Assert.AreEqual(c, 3);
        Assert.IsTrue(signal.RemoveCommand("c"));
        Assert.AreEqual(signal.CommandCount, 0);
        signal.Dispatch();
        Assert.AreEqual(a, 1);
        Assert.AreEqual(b, 2);
        Assert.AreEqual(c, 3);
    }

    [TestMethod]
    public void Command_Connect_Test()
    {
        var key1 = Rapid.InvalidKey;
        var commandData = new CommandData();
        Assert.IsTrue(
            Rapid.KeyFactoryCollection.Create(Globals.TEST_CONTEXT_NAME, out var contextKey) &&
            Rapid.KeyFactoryCollection.Create(Globals.INJECT_KEY1, out key1) &&
            Rapid.Bind(key1, contextKey, commandData));

        var signal = new TestSignalD();
        Assert.IsTrue(signal.AddCommand<TestCommandViewD>());
        Assert.AreEqual(commandData.set, false);
        Assert.IsTrue(signal.ClearCommands());

        commandData.n = 1;
        Assert.IsTrue(signal.AddCommand<TestCommandViewD>());
        Assert.AreEqual(commandData.set, true);

        Assert.IsTrue(
            Rapid.Unbind(key1, contextKey) &&
            signal.ClearCommands());
        Assert.AreEqual(Rapid.Contexts.Count, 0);
    }
    #endregion
}