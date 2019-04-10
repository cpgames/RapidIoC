using System.Collections.Generic;

namespace cpGames.core.RapidMVC.impl
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

        public bool FindContext(string name, out IContext context, out string errorMessage)
        {
            if (string.IsNullOrEmpty(name) || name.Equals(ROOT_CONTEXT_NAME))
            {
                errorMessage = string.Empty;
                context = Root;
                return true;
            }
            if (_contexts.TryGetValue(name, out context))
            {
                errorMessage = string.Empty;
                return true;
            }
            errorMessage = string.Format("Failed to find context <{0}>.", name);
            return false;
        }

        public bool FindOrCreateContext(string name, out IContext context, out string errorMessage)
        {
            if (FindContext(name, out context, out errorMessage))
            {
                return true;
            }
            var newContext = new Context(name);
            newContext.DestroyedSignal.AddCommand(() =>
            {
                _contexts.Remove(newContext.Name);
            }, true);
            _contexts.Add(name, newContext);
            context = newContext;
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