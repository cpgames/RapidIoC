namespace cpGames.core.RapidIoC.impl
{
    internal class UidKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object? keyData, out IKey key)
        {
            key = Rapid.InvalidKey;
            switch (keyData)
            {
                case long longKeyData:
                    key = new UidKey(longKeyData);
                    return Outcome.Success();
                case UidGenerator uidGenerator:
                    key = new UidKey(uidGenerator.GenerateUid());
                    return Outcome.Success();
            }
            return Outcome.Fail("keyData type is not supported.");
        }
        #endregion
    }

    internal class UidKey : IKey
    {
        #region Properties
        public long Uid { get; }
        #endregion

        #region Constructors
        public UidKey(long uid)
        {
            Uid = uid;
        }
        #endregion

        #region Methods
        protected bool Equals(UidKey other)
        {
            return Uid == other.Uid;
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
            return Equals((UidKey)obj);
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }

        public static bool operator ==(UidKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(UidKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"UidKey:{Uid}";
        }
        #endregion
    }
}