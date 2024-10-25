namespace cpGames.core.RapidIoC.Tests;

public class TestNoParamsSignal : Signal { }

public class TestOneParamSignal : Signal<int> { }

public class TestTwoParamsSignal : Signal<int, string> { }

public class TestNoParamsOutcomeSignal : SignalOutcome { }

public class TestOneParamOutcomeSignal : SignalOutcome<int> { }

public class TestTwoParamsOutcomeSignal : SignalOutcome<int, string> { }

public class TestSignalD : Signal<bool> { }