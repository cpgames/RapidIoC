namespace cpGames.core.RapidIoC.impl
{
    internal class IdKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object? keyData, out IKey key)
        {
            key = Rapid.InvalidKey;
            switch (keyData)
            {
                case Id id:
                    key = new IdKey(id);
                    return Outcome.Success();
                case byte bytes:
                    key = new IdKey(new Id(bytes));
                    return Outcome.Success();
                default: return Outcome.Fail("keyData type is not supported.");
            }
        }
        #endregion
    }

    internal class IdKey : IKey
    {
        #region Properties
        public Id Id { get; }
        #endregion

        #region Constructors
        public IdKey(Id id)
        {
            Id = id;
        }
        #endregion

        #region Methods
        protected bool Equals(IdKey other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((IdKey)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(IdKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(IdKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"IdKey:{Id}";
        }
        #endregion
    }
}