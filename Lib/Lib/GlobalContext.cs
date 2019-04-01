using System;
using System.Collections.Generic;

namespace cpGames.core.RapidMVC
{
    public class GlobalContext : Context
    {
        #region Fields
        private static GlobalContext _instance;

        private readonly Dictionary<string, LocalContext> _contexts = new Dictionary<string, LocalContext>();
        #endregion

        #region Properties
        public static GlobalContext Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                _instance = Activator.CreateInstance<GlobalContext>();
                return _instance;
            }
        }

        public override string Name => "Global";
        #endregion

        #region Methods
        public void CreateContext(string name)
        {
            if (_contexts.ContainsKey(name))
            {
                throw new Exception(string.Format("Context <{0}> already exists.", name));
            }
            _contexts.Add(name, new LocalContext(name));
        }

        public void RemoveContext(string name)
        {
            if (!_contexts.ContainsKey(name))
            {
                throw new Exception(string.Format("Context <{0}> does not exist.", name));
            }
            _contexts.Remove(name);
        }

        public Context FindContext(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this;
            }
            if (!_contexts.TryGetValue(name, out var context))
            {
                throw new Exception(string.Format("Context <{0}> does not exist.", name));
            }
            return context;
        }
        #endregion
    }
}