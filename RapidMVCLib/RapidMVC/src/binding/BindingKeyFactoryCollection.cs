using System;
using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidMVC.src
{
    internal class BindingKeyFactoryCollection : IBindingKeyFactoryCollection
    {
        #region Fields
        private readonly List<IBindingKeyFactory> _factories = new List<IBindingKeyFactory>();
        #endregion

        #region Constructors
        public BindingKeyFactoryCollection()
        {
            if (!AddFactory(new NameBindingKeyFactory(), out var errorMessage) ||
                !AddFactory(new TypeBindingKeyFactory(), out errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region IBindingKeyFactoryCollection Members
        public bool Create(object keyData, out IBindingKey key, out string errorMessage)
        {
            foreach (var factory in _factories)
            {
                if (!factory.Create(keyData, out key, out errorMessage))
                {
                    return false;
                }
                if (key != null)
                {
                    return true;
                }
            }
            key = null;
            errorMessage = "Failed to create binding key, no matching key factory found.";
            return false;
        }

        public bool AddFactory(IBindingKeyFactory factory, out string errorMessage)
        {
            if (_factories.Any(x => x.GetType() == factory.GetType()))
            {
                errorMessage = string.Format("Factory of type <{0}> already exists.", factory.GetType().Name);
                return false;
            }
            _factories.Add(factory);
            errorMessage = string.Empty;
            return true;
        }
        #endregion
    }
}