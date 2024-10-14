using System.Collections.Generic;

namespace cpGames.core.RapidIoC.impl
{
    public class ContextCollection : IContextCollection
    {
        #region Fields
        private readonly Dictionary<IKey, IContext> _contexts = new();
        #endregion

        #region IContextCollection Members
        public IContext Root { get; } = new Context(RootKey.Instance);
        public int Count => _contexts.Count;
        public IEnumerable<IContext> Contexts => _contexts.Values;

        public Outcome FindContext(IKey key, out IContext? context)
        {
            if (RootKey.Instance == key)
            {
                context = Root;
                return Outcome.Success();
            }
            return
                _contexts.TryGetValue(key, out context) ?
                    Outcome.Success() :
                    Outcome.Fail($"Failed to find context <{key}>.");
        }

        public bool TryFindContext(IKey key, out IContext? context)
        {
            if (RootKey.Instance == key)
            {
                context = Root;
                return true;
            }
            return _contexts.TryGetValue(key, out context);
        }

        public Outcome FindOrCreateContext(IKey key, out IContext? context)
        {
            if (TryFindContext(key, out context))
            {
                return Outcome.Success();
            }
            var newContext = new Context(key);
            var outcome = newContext.DestroyedSignal.AddCommand(() => { _contexts.Remove(newContext.Key); }, key, true);
            if (!outcome)
            {
                return outcome;
            }
            _contexts.Add(key, newContext);
            context = newContext;
            return Outcome.Success();
        }

        public Outcome ContextExists(IKey key)
        {
            return !_contexts.ContainsKey(key) ?
                Outcome.Fail($"Context <{key}> does not exist.") :
                Outcome.Success();
        }
        #endregion
    }
}