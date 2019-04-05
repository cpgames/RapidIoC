using System;

namespace cpGames.core.RapidMVC
{
    public class TypeBindingKeyFactory : IBindingKeyFactory
    {
        #region IBindingKeyFactory Members
        public bool Create(object keyData, out IBindingKey key, out string errorMessage)
        {
            key = null;
            if (keyData is Type typeKeyData)
            {
                try
                {
                    key = new TypeBindingKey(typeKeyData);
                }
                catch (Exception e)
                {
                    key = null;
                    errorMessage = e.Message;
                    return false;
                }
            }
            errorMessage = string.Empty;
            return true;
        }
        #endregion
    }

    public class TypeBindingKey : IBindingKey
    {
        #region Properties
        public Type Type { get; }
        #endregion

        #region Constructors
        public TypeBindingKey(Type type)
        {
            Type = type;
        }
        #endregion

        #region Methods
        protected bool Equals(TypeBindingKey other)
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
            return Equals((TypeBindingKey)obj);
        }

        public override int GetHashCode()
        {
            return Type != null ? Type.GetHashCode() : 0;
        }

        public static bool operator ==(TypeBindingKey lhs, IBindingKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(TypeBindingKey lhs, IBindingKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return string.Format("TypeBindingKey:{0}", Type.Name);
        }
        #endregion
    }
}