namespace cpGames.core.RapidIoC.Tests
{
    public class TestSignalA : Signal { }

    public class TestSignalB : Signal<int> { }

    public class TestSignalC : Signal<int, string> { }
}