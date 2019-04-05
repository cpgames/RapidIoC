using System.Collections.Generic;

namespace cpGames.core.RapidMVC
{
    public interface IContextCollection
    {
        #region Properties
        IContext Root { get; }
        int Count { get; }
        IEnumerable<IContext> Contexts { get; }
        #endregion

        #region Methods
        bool Find(string name, out IContext context, out string errorMessage);
        bool FindOrCreate(string name, out IContext context, out string errorMessage);
        bool Exists(string name);
        #endregion
    }

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

        public bool Find(string name, out IContext context, out string errorMessage)
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

        public bool FindOrCreate(string name, out IContext context, out string errorMessage)
        {
            if (Find(name, out context, out errorMessage))
            {
                return true;
            }
            var newContext = new Context(name);
            newContext.DestroyedSignal.AddOnce(() =>
            {
                _contexts.Remove(newContext.Name);
            });
            _contexts.Add(name, newContext);
            context = newContext;
            errorMessage = string.Empty;
            return true;
        }

        public bool Exists(string name)
        {
            return Find(name, out _, out _);
        }
        #endregion
    }
}