using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cpGames.core.RapidMVC.Tests
{
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
            }, null, true);
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
            var commandData = new CommandData();
            Rapid.Bind(Globals.INJECT_KEY1, commandData, Globals.TEST_CONTEXT_NAME);
            var signal = new TestSignalA();
            signal.AddCommand<TestCommandViewA>();
            signal.Dispatch();
            Assert.AreEqual(commandData.n, 1);
            signal.Dispatch();
            Assert.AreEqual(commandData.n, 2);
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            signal.ClearCommands();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void OneParam_CommandView_Test()
        {
            var commandData = new CommandData();
            Rapid.Bind(Globals.INJECT_KEY1, commandData, Globals.TEST_CONTEXT_NAME);
            var signal = new TestSignalB();
            signal.AddCommand<TestCommandViewB>();
            signal.Dispatch(5);
            Assert.AreEqual(commandData.n, 5);
            signal.Dispatch(4);
            Assert.AreEqual(commandData.n, 9);
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            signal.ClearCommands();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void TwoParam_CommandView_Test()
        {
            var commandData = new CommandData();
            Rapid.Bind(Globals.INJECT_KEY1, commandData, Globals.TEST_CONTEXT_NAME);
            var signal = new TestSignalC();
            signal.AddCommand<TestCommandViewC>();
            signal.Dispatch(5, "five");
            Assert.AreEqual(commandData.n, 5);
            Assert.AreEqual(commandData.t, "five");
            signal.Dispatch(4, "four");
            Assert.AreEqual(commandData.n, 9, "four");
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            signal.ClearCommands();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void Duplicate_Command_Test()
        {
            var commandData = new CommandData();
            Rapid.Bind(Globals.INJECT_KEY1, commandData, Globals.TEST_CONTEXT_NAME);
            var signal = new TestSignalA();
            signal.AddCommand<TestCommandViewA>();
            Assert.ThrowsException<Exception>(() =>
            {
                signal.AddCommand<TestCommandViewA>();
            });
            Assert.AreEqual(signal.CommandCount, 1);
            signal.Dispatch();
            Assert.AreEqual(commandData.n, 1);
            Rapid.Unbind(Globals.INJECT_KEY1, Globals.TEST_CONTEXT_NAME);
            signal.ClearCommands();
            Assert.AreEqual(Rapid.Contexts.Count, 0);
        }

        [TestMethod]
        public void Dispatch_While_Modifying_Commands_Test()
        {
            var n = 0;
            var signal = new TestSignalA();
            signal.AddCommand(() =>
            {
                signal.AddCommand(() =>
                {
                    n = 5;
                });
            });
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
            signal.AddCommand(() =>
            {
                a++;
            }, "a");
            signal.AddCommand(() =>
            {
                b++;
            }, "b");
            signal.AddCommand(() =>
            {
                c++;
            }, "c");
            Assert.AreEqual(signal.CommandCount, 3);
            signal.Dispatch();
            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 1);
            Assert.AreEqual(c, 1);
            signal.RemoveCommand("a");
            Assert.AreEqual(signal.CommandCount, 2);
            signal.Dispatch();
            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 2);
            Assert.AreEqual(c, 2);
            signal.RemoveCommand("b");
            Assert.AreEqual(signal.CommandCount, 1);
            signal.Dispatch();
            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 2);
            Assert.AreEqual(c, 3);
            signal.RemoveCommand("c");
            Assert.AreEqual(signal.CommandCount, 0);
            signal.Dispatch();
            Assert.AreEqual(a, 1);
            Assert.AreEqual(b, 2);
            Assert.AreEqual(c, 3);
        }
        #endregion
    }
}