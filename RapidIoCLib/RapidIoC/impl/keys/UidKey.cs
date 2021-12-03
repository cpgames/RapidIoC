﻿namespace cpGames.core.RapidIoC.impl
{
    internal class UidKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public bool Create(object keyData, out IKey key)
        {
            key = null;
            switch (keyData)
            {
                case long longKeyData:
                    key = new UidKey(longKeyData);
                    return true;
                case UidGenerator uidGenerator:
                    key = new UidKey(uidGenerator.GenerateUid());
                    return true;
            }
            return false;
        }

        public bool Create(object keyData, out IKey key, out string errorMessage)
        {
            if (!Create(keyData, out key))
            {
                errorMessage = "keyData type is not supported.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
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
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
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