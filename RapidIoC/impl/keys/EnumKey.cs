using System;

namespace cpGames.core.RapidIoC.impl
{
    internal class EnumKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object? keyData, out IKey key)
        {
            key = Rapid.InvalidKey;
            if (keyData != null && keyData.GetType().IsEnum)
            {
                key = new EnumKey((Enum)keyData);
                return Outcome.Success();
            }
            return Outcome.Fail("keyData type is not supported.");
        }
        #endregion
    }

    internal class EnumKey : IKey
    {
        #region Properties
        public Enum EnumType { get; }
        #endregion

        #region Constructors
        public EnumKey(Enum enumType)
        {
            EnumType = enumType;
        }
        #endregion

        #region Methods
        protected bool Equals(EnumKey other)
        {
            return Enum.Equals(EnumType, other.EnumType);
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
            return Equals((EnumKey)obj);
        }

        public override int GetHashCode()
        {
            return EnumType.GetHashCode();
        }

        public static bool operator ==(EnumKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(EnumKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"EnumKey:{EnumType}";
        }
        #endregion
    }
}