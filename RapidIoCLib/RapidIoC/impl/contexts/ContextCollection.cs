using System.Collections.Generic;

namespace cpGames.core.RapidIoC.impl
{
    public class ContextCollection : IContextCollection
    {
        #region Fields
        public static readonly string ROOT_CONTEXT_NAME = "Root";
        private readonly Dictionary<string, IContext> _contexts = new Dictionary<string, IContext>();
        #endregion

        #region IContextCollection Members
        public IContext Root { get; } = new Context(ROOT_CONTEXT_NAME);
        public int Count => _contexts.Count;
        public IEnumerable<IContext> Contexts => _contexts.Values;

        public bool FindContext(string name, out IContext context)
        {
            if (string.IsNullOrEmpty(name) || name.Equals(ROOT_CONTEXT_NAME))
            {
                context = Root;
                return true;
            }
            return _contexts.TryGetValue(name, out context);
        }

        public bool FindContext(string name, out IContext context, out string errorMessage)
        {
            if (!FindContext(name, out context))
            {
                errorMessage = $"Failed to find context <{name}>.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool FindOrCreateContext(string name, out IContext context)
        {
            if (FindContext(name, out context))
            {
                return true;
            }
            var newContext = new Context(name);
            newContext.DestroyedSignal.AddCommand(() =>
            {
                _contexts.Remove(newContext.Name);
            }, name, true);
            _contexts.Add(name, newContext);
            context = newContext;
            return true;
        }

        public bool FindOrCreateContext(string name, out IContext context, out string errorMessage)
        {
            if (!FindOrCreateContext(name, out context))
            {
                errorMessage = $"Failed to find or create context <{name}>.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public bool ContextExists(string name)
        {
            return FindContext(name, out _, out _);
        }
        #endregion
    }
}