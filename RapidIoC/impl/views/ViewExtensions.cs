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

        public static Outcome ConnectSignalProperty(this IView view, PropertyInfo signalProperty)
        {
            var signal = (SignalBase?)signalProperty.GetValue(view, null);
            if (signal == null)
            {
                return Outcome.Success();
            }
            var baseName = SignalToBaseName(signalProperty.Name);
            var signalType = signal.GetType();
            var interfaces = signalType.GetInterfaces();

            foreach (var interfaceType in interfaces)
            {
                if (interfaceType.IsGenericType)
                {
                    var genericTypeDefinition = interfaceType.GetGenericTypeDefinition();
                    var genericArguments = interfaceType.GetGenericArguments();

                    if (genericTypeDefinition == typeof(ISignalResult<>))
                    {
                        return ConnectSignalResultWithNoParameters(view, signal, baseName, genericArguments);
                    }
                    if (genericTypeDefinition == typeof(ISignalResult<,>))
                    {
                        return ConnectSignalResultWithOneParameter(view, signal, baseName, genericArguments);
                    }
                    if (genericTypeDefinition == typeof(ISignalResult<,,>))
                    {
                        return ConnectSignalResultWithTwoParameters(view, signal, baseName, genericArguments);
                    }
                    if (genericTypeDefinition == typeof(ISignal<>))
                    {
                        return ConnectSignalWithOneParameter(view, signal, baseName, genericArguments);
                    }
                    if (genericTypeDefinition == typeof(ISignal<,>))
                    {
                        return ConnectSignalWithTwoParameters(view, signal, baseName, genericArguments);
                    }
                }
                else if (interfaceType == typeof(ISignal))
                {
                    return ConnectSignalWithNoParameters(view, signal, baseName);
                }
            }

            return Outcome.Success();
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
                    !x.HasAttribute<IgnoreSignalMapAttribute>() &&
                    x.GetParameters().Length == 0 &&
                    x.ReturnType == typeof(void))
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(Action);
                var action = (Action)Delegate.CreateDelegate(actionType, view, method);
                return (signal as ISignal)!.AddCommand(action, view);
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalResultWithNoParameters(
            IView view,
            SignalBase signal,
            string baseName,
            Type[] arguments)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>() &&
                    x.GetParameters().Length == 0 &&
                    x.ReturnType == arguments[0])
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(ActionResultDelegate<>).MakeGenericType(arguments[0]);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var addCommandMethod = signal.GetType().GetMethod("AddCommand", new[] { actionType, typeof(object), typeof(bool) });
                if (addCommandMethod != null)
                {
                    var outcome = (Outcome)addCommandMethod.Invoke(signal, new object[] { action, view, false });
                    return outcome;
                }
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalWithOneParameter(
            IView view,
            SignalBase signal,
            string baseName,
            Type[] arguments)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>() &&
                    x.GetParameters().Length == 1 &&
                    x.GetParameters()[0].ParameterType == arguments[0] &&
                    x.ReturnType == typeof(void))
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(Action<>).MakeGenericType(arguments[0]);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var commandType = typeof(ActionCommand<>).MakeGenericType(arguments[0]);
                var command = Activator.CreateInstance(commandType, action);
                var addCommandMethod = signal.GetType().GetMethod("AddCommand", new[] { commandType, typeof(object), typeof(bool) });
                if (addCommandMethod != null)
                {
                    var outcome = (Outcome)addCommandMethod.Invoke(signal, new[] { command, view, false });
                    return outcome;
                }
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalResultWithOneParameter(
            IView view,
            SignalBase signal,
            string baseName,
            Type[] arguments)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>() &&
                    x.GetParameters().Length == 1 &&
                    x.GetParameters()[0].ParameterType == arguments[1] &&
                    x.ReturnType == arguments[0])
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(ActionResultDelegate<,>).MakeGenericType(arguments[0], arguments[1]);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var addCommandMethod = signal.GetType().GetMethod("AddCommand", new[] { actionType, typeof(object), typeof(bool) });
                if (addCommandMethod != null)
                {
                    var outcome = (Outcome)addCommandMethod.Invoke(signal, new object[] { action, view, false });
                    return outcome;
                }
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalWithTwoParameters(
            IView view,
            SignalBase signal,
            string baseName,
            Type[] arguments)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>() &&
                    x.GetParameters().Length == 2 &&
                    x.GetParameters()[0].ParameterType == arguments[0] &&
                    x.GetParameters()[1].ParameterType == arguments[1] &&
                    x.ReturnType == typeof(void))
                .FirstOrDefault(x => x.Name.Equals(methodName));

            if (method != null)
            {
                var actionType = typeof(Action<,>).MakeGenericType(arguments);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var commandType = typeof(ActionCommand<,>).MakeGenericType(arguments);
                var command = Activator.CreateInstance(commandType, action);
                var addCommandMethod = signal.GetType().GetMethod("AddCommand", new[] { commandType, typeof(object), typeof(bool) });
                if (addCommandMethod != null)
                {
                    var outcome = (Outcome)addCommandMethod.Invoke(signal, new[] { command, view, false });
                    return outcome;
                }
            }
            return Outcome.Success();
        }

        private static Outcome ConnectSignalResultWithTwoParameters(
            IView view,
            SignalBase signal,
            string baseName,
            Type[] arguments)
        {
            var type = view.GetType();
            var methodName = "On" + baseName;
            var method = type.GetMethods(BINDING_FLAGS)
                .Where(x =>
                    !x.HasAttribute<IgnoreSignalMapAttribute>() &&
                    x.GetParameters().Length == 2 &&
                    x.GetParameters()[0].ParameterType == arguments[1] &&
                    x.GetParameters()[1].ParameterType == arguments[2] &&
                    x.ReturnType == arguments[0])
                .FirstOrDefault(x => x.Name.Equals(methodName));
            if (method != null)
            {
                var actionType = typeof(ActionResultDelegate<,,>).MakeGenericType(arguments[0], arguments[1], arguments[2]);
                var action = Delegate.CreateDelegate(actionType, view, method);
                var addCommandMethod = signal.GetType().GetMethod("AddCommand", new[] { actionType, typeof(object), typeof(bool) });
                if (addCommandMethod != null)
                {
                    var outcome = (Outcome)addCommandMethod.Invoke(signal, new object[] { action, view, false });
                    return outcome;
                }
            }
            return Outcome.Success();
        }
        #endregion
    }
}