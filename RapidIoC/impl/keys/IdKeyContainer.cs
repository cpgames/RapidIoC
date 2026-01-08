using System.Collections.Generic;

namespace cpGames.core.RapidIoC.impl
{
    internal class IdKeyContainer : IIdProvider
    {
        #region Fields
        private const byte ID_SIZE = 4;
        protected readonly object _syncRoot = new();
        private readonly HashSet<Id> _ids = new();
        private readonly IdGenerator _generator = new();
        #endregion

        #region IIdProvider Members
        public byte IdSize => ID_SIZE;

        public bool HasId(Id id)
        {
            lock (_syncRoot)
            {
                return _ids.Contains(id);
            }
        }
        #endregion

        #region Methods
        public Outcome CreateKey(out IKey key)
        {
            lock (_syncRoot)
            {
                key = Rapid.InvalidKey;
                var generateIdResult =
                    _generator.GenerateId(this, out var id) &&
                    Rapid.KeyFactoryCollection.Create(id, out key);
                if (!generateIdResult)
                {
                    return generateIdResult;
                }
                _ids.Add(id);
                return Outcome.Success();
            }
        }

        public Outcome RemoveKey(IdKey key)
        {
            lock (_syncRoot)
            {
                return _ids.Remove(key.Id) ?
                    Outcome.Success() :
                    Outcome.Fail($"Id <{key.Id}> does not exist.");
            }
        }
        #endregion
    }
}