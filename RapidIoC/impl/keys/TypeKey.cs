using System;

namespace cpGames.core.RapidIoC
{
    public class TypeKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object keyData, out IKey? key)
        {
            key = null;
            if (keyData is Type typeKeyData)
            {
                key = new TypeKey(typeKeyData);
                return Outcome.Success();
            }
            return Outcome.Fail("keyData type is not supported.");
        }
        #endregion
    }

    public class TypeKey : IKey
    {
        #region Properties
        public Type Type { get; }
        #endregion

        #region Constructors
        public TypeKey(Type type)
        {
            Type = type;
        }
        #endregion

        #region Methods
        protected bool Equals(TypeKey other)
        {
            return Type == other.Type;
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
            return Equals((TypeKey)obj);
        }

        public override int GetHashCode()
        {
            return Type != null ? Type.GetHashCode() : 0;
        }

        public static bool operator ==(TypeKey lhs, IKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(TypeKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"TypeKey:{Type.Name}";
        }
        #endregion
    }
}