namespace cpGames.core.RapidIoC.Tests;

[TestClass]
public class Rapid_Binding_Tests
{
    [TestMethod]
    public void Bind_And_Unbind_Test()
    {
        var bindOutcome = Rapid.Bind<TestView>(Rapid.RootKey);
        Assert.IsTrue(bindOutcome);
        var unbindOutcome = Rapid.Unbind<TestView>(Rapid.RootKey);
        Assert.IsTrue(unbindOutcome);

        bindOutcome = Rapid.Bind<TestView>(Rapid.RootKey);
        Assert.IsTrue(bindOutcome);
        unbindOutcome = Rapid.Unbind<TestView>(Rapid.RootKey);
        Assert.IsTrue(unbindOutcome);
    }
}