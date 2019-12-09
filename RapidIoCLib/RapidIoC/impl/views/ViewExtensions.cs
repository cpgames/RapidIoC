using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cpGames.core.CpReflection;

namespace cpGames.core.RapidIoC.impl
{
    internal class IgnoreSignalMapAttribute : Attribute { }

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

        public static bool GetInjectionKey(this PropertyInfo property, out IKey key, out string errorMessage)
        {
            var keyData = property.GetAttribute<InjectAttribute>().Key ?? property.PropertyType;
            return Rapid.KeyFactoryCollection.Create(keyData, out key, out errorMessage);
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

        private static Type GetSignalType(BaseSignal signal)
        {
            var signalType = signal.GetType();
            while (signalType.BaseType != typeof(BaseSignal))
            {
                signalType = signalType.BaseType;
            }
            return signalType;
        }

        public static void ConnectSignalProperty(this IView view, PropertyInfo signalProperty)
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
                    ConnectSignalWithNoParameters(view, signal, baseName);
                    break;
                case 1:
                    ConnectSignalWithOneParameter(view, signal, baseName);
                    break;
                case 2:
                    ConnectSignalWithTwoParameters(view, signal, baseName);
                    break;
            }
        }

        private static void ConnectSignalWithNoParameters(
            IView view,
            BaseSignal signal,
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
                var command = new ActionCommand(action);
                (signal as Signal).AddCommand(command, view);
            }
        }

        private static void ConnectSignalWithOneParameter(
            IView view,
            BaseSignal signal,
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
                addCommandMethod.Invoke(signal, new[] { command, view, false });
            }
        }

        private static void ConnectSignalWithTwoParameters(
            IView view,
            BaseSignal signal,
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
                addCommandMethod.Invoke(signal, new[] { command, view, false });
            }
        }
        #endregion
    }
}