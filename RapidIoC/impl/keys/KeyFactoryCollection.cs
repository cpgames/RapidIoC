﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace cpGames.core.RapidIoC.impl
{
    internal class KeyFactoryCollection : IKeyFactoryCollection
    {
        #region Fields
        private readonly List<IKeyFactory> _factories = new();
        #endregion

        #region Constructors
        public KeyFactoryCollection()
        {
            var addFactoryOutcome =
                AddFactory(new NameKeyFactory()) &&
                AddFactory(new TypeKeyFactory()) &&
                AddFactory(new IdKeyFactory()) &&
                AddFactory(new AddressKeyFactory()) &&
                //AddFactory(new ByteKeyFactory()) && // raw bytes are converted into IdKeys, comment out IdKeyFactory to use ByteKeyFactory instead
                AddFactory(new EnumKeyFactory()) &&
                AddFactory(new CompositeKeyFactory()) &&
                AddFactory(new InstanceKeyFactory());

            if (!addFactoryOutcome)
            {
                throw new Exception(addFactoryOutcome.ErrorMessage);
            }
        }
        #endregion

        #region IKeyFactoryCollection Members
        public Outcome Create(object? keyData, out IKey key)
        {
            if (keyData is IKey)
            {
                key = (IKey)keyData;
                return Outcome.Success();
            }

            lock (_factories)
            {
                foreach (var factory in _factories)
                {
                    lock (factory)
                    {
                        if (!factory.CanCreate(keyData))
                        {
                            continue;
                        }
                        return factory.Create(keyData, out key);
                    }
                }
            }
            key = Rapid.InvalidKey;
            return Outcome.Fail("Failed to create binding key, no matching key factory found.");
        }

        public Outcome AddFactory(IKeyFactory factory)
        {
            lock (_factories)
            {
                if (_factories.Any(x => x.GetType() == factory.GetType()))
                {
                    return Outcome.Fail($"Factory of type <{factory.GetType().Name}> already exists.");
                }
                _factories.Add(factory);
            }
            return Outcome.Success();
        }
        #endregion
    }
}