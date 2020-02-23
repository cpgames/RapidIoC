using System;

namespace cpGames.core.RapidIoC.impl
{
    internal class EnumKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public bool Create(object keyData, out IKey key, out string errorMessage)
        {
            key = null;
            errorMessage = string.Empty;
            if (keyData.GetType().IsEnum)
            {
                key = new EnumKey((Enum)keyData);
                return true;
            }
            errorMessage = "keyData type is not supported.";
            return false;
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
            return EnumType != null ? EnumType.GetHashCode() : 0;
        }

        public static bool operator ==(EnumKey lhs, IKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(EnumKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return string.Format("EnumKey:{0}", EnumType);
        }
        #endregion
    }
}