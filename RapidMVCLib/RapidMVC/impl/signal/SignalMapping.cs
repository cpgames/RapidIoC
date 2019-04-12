using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cpGames.core.CpReflection;

namespace cpGames.core.RapidMVC.impl
{
    public interface ISignalMapping
    {
        #region Properties
        string Name { get; }
        #endregion

        #region Methods
        void Unregister();
        #endregion
    }

    internal class SignalMap : ISignalMapping
    {
        #region Fields
        private readonly Signal _signal;
        private readonly List<Command> _commands;
        #endregion

        #region Constructors
        public SignalMap(string name, Signal signal)
        {
            Name = name;
            _signal = signal;
            _commands = new List<Command>();
        }
        #endregion

        #region ISignalMapping Members
        public string Name { get; }

        public void Unregister()
        {
            _commands.ForEach(x => _signal.RemoveCommand(x));
        }
        #endregion

        #region Methods
        public void RegisterCommand(Command command)
        {
            _signal.AddCommand(command);
            _commands.Add(command);
        }
        #endregion
    }

    internal class SignalMap<T> : ISignalMapping
    {
        #region Fields
        private readonly Signal<T> _signal;
        private readonly List<Command<T>> _commands;
        #endregion

        #region Constructors
        public SignalMap(string name, Signal<T> signal)
        {
            Name = name;
            _signal = signal;
            _commands = new List<Command<T>>();
        }
        #endregion

        #region ISignalMapping Members
        public string Name { get; }

        public void Unregister()
        {
            _commands.ForEach(x => _signal.RemoveCommand(x));
        }
        #endregion

        #region Methods
        public void RegisterCommand(Command<T> command)
        {
            _signal.AddCommand(command);
            _commands.Add(command);
        }
        #endregion
    }

    internal class SignalMap<T1, T2> : ISignalMapping
    {
        #region Fields
        private readonly Signal<T1, T2> _signal;
        private readonly List<Command<T1, T2>> _commands;
        #endregion

        #region Constructors
        public SignalMap(string name, Signal<T1, T2> signal)
        {
            Name = name;
            _signal = signal;
            _commands = new List<Command<T1, T2>>();
        }
        #endregion

        #region ISignalMapping Members
        public string Name { get; }

        public void Unregister()
        {
            _commands.ForEach(x => _signal.RemoveCommand(x));
        }
        #endregion

        #region Methods
        public void RegisterCommand(Command<T1, T2> command)
        {
            _signal.AddCommand(command);
            _commands.Add(command);
        }
        #endregion
    }

    internal class IgnoreSignalMapAttribute : Attribute { }

    internal static class SignalMapping
    {
        #region Fields
        private const BindingFlags BINDING_FLAGS =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy;
        #endregion

        #region Methods
        public static void RegisterSignalMap(this IView view, PropertyInfo signalProperty)
        {
            var signalValue = (BaseSignal)signalProperty.GetValue(view, null);
            if (signalValue == null)
            {
                return;
            }
            var baseName = SignalToBaseName(signalProperty.Name);
            switch (signalProperty.PropertyType.GetGenericArguments().Length)
            {
                case 0:
                    MapSignalWithNoParameters(view, signalValue, baseName);
                    break;
                case 1:
                    MapSignalWithOneParameter(view, signalValue, baseName);
                    break;
                case 2:
                    MapSignalWithTwoParameters(view, signalValue, baseName);
                    break;
            }
        }

        public static void UnregisterAllSignalMaps(this IView view)
        {
            view.SignalMappings.ForEach(x => x.Unregister());
            view.SignalMappings.Clear();
        }

        public static void UnregisterSignalMap(this IView view, PropertyInfo signalProperty)
        {
            var baseName = SignalToBaseName(signalProperty.Name);
            var mapping = view.SignalMappings.FirstOrDefault(x => x.Name == baseName);
            if (mapping != null)
            {
                mapping.Unregister();
                view.SignalMappings.Remove(mapping);
            }
        }

        private static void MapSignalWithNoParameters(
            IView view,
            BaseSignal signal,
            string baseName)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x => x.GetParameters().Length == 0)
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var signalMapping = view.SignalMappings.FirstOrDefault(x => x.Name.Equals(baseName));
                if (signalMapping == null)
                {
                    var mappingType = typeof(SignalMap);
                    signalMapping = (ISignalMapping)Activator.CreateInstance(mappingType, baseName, signal);
                    view.SignalMappings.Add(signalMapping);
                }
                var actionType = typeof(Action);
                var action = (Action)Delegate.CreateDelegate(actionType, view, method);
                var command = new ActionCommand(action);
                ReflectionCommon.InvokeMethod(signalMapping, "RegisterCommand", new object[] { command });
            }
        }

        private static void MapSignalWithOneParameter(
            IView view,
            BaseSignal signal,
            string baseName)
        {
            var type = view.GetType();
            var signalType = signal.GetType();
            while (signalType.BaseType != typeof(BaseSignal))
            {
                signalType = signalType.BaseType;
            }
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x => x.GetParameters().Length == 1)
                .Where(x => x.GetParameters()[0].ParameterType == signalType.GetGenericArguments()[0])
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var signalMapping = view.SignalMappings.FirstOrDefault(x => x.Name.Equals(baseName));
                if (signalMapping == null)
                {
                    var mappingType = typeof(SignalMap<>).MakeGenericType(signalType.GetGenericArguments()[0]);
                    signalMapping = (ISignalMapping)Activator.CreateInstance(mappingType, baseName, signal);
                    view.SignalMappings.Add(signalMapping);
                }
                var actionType = typeof(Action<>).MakeGenericType(signalType.GetGenericArguments()[0]);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var commandType = typeof(ActionCommand<>).MakeGenericType(signalType.GetGenericArguments()[0]);
                var command = Activator.CreateInstance(commandType, action);
                ReflectionCommon.InvokeMethod(signalMapping, "RegisterCommand", new[] { command });
            }
        }

        private static void MapSignalWithTwoParameters(
            IView view,
            BaseSignal signal,
            string baseName)
        {
            var type = view.GetType();
            var signalType = signal.GetType();
            while (signalType.BaseType != typeof(BaseSignal))
            {
                signalType = signalType.BaseType;
            }
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x => x.GetParameters().Length == 2)
                .Where(x => x.GetParameters()[0].ParameterType == signalType.GetGenericArguments()[0]
                    && x.GetParameters()[1].ParameterType == signalType.GetGenericArguments()[1])
                .FirstOrDefault(x => x.Name.Equals(methodName));

            if (method != null)
            {
                var signalMapping = view.SignalMappings.FirstOrDefault(x => x.Name.Equals(baseName));
                if (signalMapping == null)
                {
                    var mappingType = typeof(SignalMap<,>).MakeGenericType(signalType.GetGenericArguments());
                    signalMapping = (ISignalMapping)Activator.CreateInstance(mappingType, baseName, signal);
                    view.SignalMappings.Add(signalMapping);
                }
                var actionType = typeof(Action<,>).MakeGenericType(signalType.GetGenericArguments());
                var action = Delegate.CreateDelegate(actionType, view, method);
                var commandType = typeof(ActionCommand<,>).MakeGenericType(signalType.GetGenericArguments());
                var command = Activator.CreateInstance(commandType, action);
                ReflectionCommon.InvokeMethod(signalMapping, "RegisterCommand", new[] { command });
            }
        }

        public static string SignalToBaseName(string signalName)
        {
            var baseName = signalName;
            if (baseName.EndsWith("signal", StringComparison.CurrentCultureIgnoreCase))
            {
                baseName = baseName.Substring(0, baseName.Length - 6);
            }
            return baseName;
        }
        #endregion
    }
}