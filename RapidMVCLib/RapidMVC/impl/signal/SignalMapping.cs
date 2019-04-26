using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cpGames.core.CpReflection;

namespace cpGames.core.RapidMVC.impl
{
    /// <summary>
    /// Signal mapping automatically connects injected signals to matching listener member methods.
    /// Signal mapping works by extracting the name of a signal property and finding a method that
    /// matches the name pattern and parameters of a signal.
    /// To be able to automap, signal property must be named with "Signal" suffix, e.g. LevelLoadedSignal.
    /// Method must contain same base name, but instead have "On" prefix, e.g. OnLevelLoaded.
    /// </summary>
    public interface ISignalMapping
    {
        #region Properties
        string Name { get; }
        #endregion

        #region Methods
        void Unregister();
        #endregion
    }

    internal abstract class BaseSignalMap : ISignalMapping
    {
        #region Fields
        protected readonly List<IKey> _localCommands = new List<IKey>();
        #endregion

        #region Properties
        protected abstract BaseSignal Signal { get; }
        #endregion

        #region Constructors
        protected BaseSignalMap(string name)
        {
            Name = name;
        }
        #endregion

        #region ISignalMapping Members
        public string Name { get; }

        public void Unregister()
        {
            foreach (var command in _localCommands)
            {
                Signal.RemoveCommand(command);
            }
        }
        #endregion
    }

    internal class SignalMap : BaseSignalMap
    {
        #region Fields
        private readonly Signal _signal;
        #endregion

        #region Properties
        protected override BaseSignal Signal => _signal;
        #endregion

        #region Constructors
        public SignalMap(string name, Signal signal) : base(name)
        {
            _signal = signal;
        }
        #endregion

        #region Methods
        public void RegisterCommand(Command command)
        {
            _localCommands.Add(_signal.AddCommand(command));
        }
        #endregion
    }

    internal class SignalMap<T> : BaseSignalMap
    {
        #region Fields
        private readonly Signal<T> _signal;
        #endregion

        #region Properties
        protected override BaseSignal Signal => _signal;
        #endregion

        #region Constructors
        public SignalMap(string name, Signal<T> signal) : base(name)
        {
            _signal = signal;
        }
        #endregion

        #region Methods
        public void RegisterCommand(Command<T> command)
        {
            _localCommands.Add(_signal.AddCommand(command));
        }
        #endregion
    }

    internal class SignalMap<T1, T2> : BaseSignalMap
    {
        #region Fields
        private readonly Signal<T1, T2> _signal;
        #endregion

        #region Properties
        protected override BaseSignal Signal => _signal;
        #endregion

        #region Constructors
        public SignalMap(string name, Signal<T1, T2> signal) : base(name)
        {
            _signal = signal;
        }
        #endregion

        #region Methods
        public void RegisterCommand(Command<T1, T2> command)
        {
            _localCommands.Add(_signal.AddCommand(command));
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
            var signal = (BaseSignal)signalProperty.GetValue(view, null);
            if (signal == null)
            {
                return;
            }
            var baseName = SignalToBaseName(signalProperty.Name);
            var signalType = GetSignalType(signal);
            switch (signalType.GetGenericArguments().Length)
            {
                case 0:
                    MapSignalWithNoParameters(view, signal, baseName);
                    break;
                case 1:
                    MapSignalWithOneParameter(view, signal, baseName);
                    break;
                case 2:
                    MapSignalWithTwoParameters(view, signal, baseName);
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
            var signalType = GetSignalType(signal);
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
            var signalType = GetSignalType(signal);
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

        private static Type GetSignalType(BaseSignal signal)
        {
            var signalType = signal.GetType();
            while (signalType.BaseType != typeof(BaseSignal))
            {
                signalType = signalType.BaseType;
            }
            return signalType;
        }
        #endregion
    }
}