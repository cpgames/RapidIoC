using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace cpGames.core.RapidIoC.impl
{
    internal static class ViewExtensions
    {
        #region Fields
        private const BindingFlags BINDING_FLAGS =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.FlattenHierarchy;
        #endregion

        #region Methods
        public static IEnumerable<PropertyInfo> GetInjectedProperties(this IView view)
        {
            return view.GetType().GetProperties().Where(x => x.HasAttribute<InjectAttribute>());
        }

        public static Outcome GetInjectionKey(this PropertyInfo property, out IKey key)
        {
            var keyData = property.GetAttribute<InjectAttribute>()?.KeyData ?? property.PropertyType;
            return Rapid.KeyFactoryCollection.Create(keyData, out key);
        }

        private static string SignalToBaseName(string signalName)
        {
            var baseName = signalName;
            if (baseName.EndsWith("signal", StringComparison.CurrentCultureIgnoreCase))
            {
                baseName = baseName.Substring(0, baseName.Length - 6);
            }
            return baseName;
        }

        private static Type GetSignalType(SignalBase signal)
        {
            var signalType = signal.GetType();
            while (signalType.BaseType != typeof(SignalBase))
            {
                signalType = signalType.BaseType;
            }
            return signalType;
        }

        public static Outcome ConnectSignalProperty(this IView view, PropertyInfo signalProperty)
        {
            var signal = (SignalBase?)signalProperty.GetValue(view, null);
            if (signal == null)
            {
                return Outcome.Success();
            }
            var baseName = SignalToBaseName(signalProperty.Name);
            var signalType = GetSignalType(signal);
            var argLength = signalType.GetGenericArguments().Length;
            switch (argLength)
            {
                case 0:
                    return ConnectSignalWithNoParameters(view, signal, baseName);
                case 1:
                    return ConnectSignalWithOneParameter(view, signal, baseName);
                case 2:
                    return ConnectSignalWithTwoParameters(view, signal, baseName);
            }
            return Outcome.Fail("Only up to 2 parameters are supported.");
        }

        private static Outcome ConnectSignalWithNoParameters(
            IView view,
            SignalBase signal,
            string baseName)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>()
                    && x.GetParameters().Length == 0)
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(Action);
                var action = (Action)Delegate.CreateDelegate(actionType, view, method);
                return (signal as ISignal)!.AddCommand(action, view);
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalWithOneParameter(
            IView view,
            SignalBase signal,
            string baseName)
        {
            var type = view.GetType();
            var signalType = GetSignalType(signal);
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>()
                    && x.GetParameters().Length == 1
                    && x.GetParameters()[0].ParameterType == signalType.GetGenericArguments()[0])
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(Action<>).MakeGenericType(signalType.GetGenericArguments()[0]);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var commandType = typeof(ActionCommand<>).MakeGenericType(signalType.GetGenericArguments()[0]);
                var command = Activator.CreateInstance(commandType, action);
                var addCommandMethod = signalType.GetMethod("AddCommand", new[] { commandType, typeof(object), typeof(bool) });
                var outcome = (Outcome)addCommandMethod.Invoke(signal, new[] { command, view, false });
                return outcome;
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalWithTwoParameters(
            IView view,
            SignalBase signal,
            string baseName)
        {
            var type = view.GetType();
            var signalType = GetSignalType(signal);
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>()
                    && x.GetParameters().Length == 2
                    && x.GetParameters()[0].ParameterType == signalType.GetGenericArguments()[0]
                    && x.GetParameters()[1].ParameterType == signalType.GetGenericArguments()[1])
                .FirstOrDefault(x => x.Name.Equals(methodName));

            if (method != null)
            {
                var actionType = typeof(Action<,>).MakeGenericType(signalType.GetGenericArguments());
                var action = Delegate.CreateDelegate(actionType, view, method);
                var commandType = typeof(ActionCommand<,>).MakeGenericType(signalType.GetGenericArguments());
                var command = Activator.CreateInstance(commandType, action);
                var addCommandMethod = signalType.GetMethod("AddCommand", new[] { commandType, typeof(object), typeof(bool) });
                var outcome = (Outcome)addCommandMethod.Invoke(signal, new[] { command, view });
                return outcome;
            }
            return Outcome.Success();
        }
        #endregion
    }
}