using System;

namespace cpGames.core.RapidMVC
{
    public interface IBindingKey { }

    public class NameBindingKey : IBindingKey
    {
        #region Properties
        public string Name { get; }
        #endregion

        #region Constructors
        public NameBindingKey(string name)
        {
            Name = name;
        }
        #endregion

        #region Methods
        protected bool Equals(NameBindingKey other)
        {
            return string.Equals(Name, other.Name);
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
            return Equals((NameBindingKey)obj);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public static bool operator ==(NameBindingKey lhs, IBindingKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(NameBindingKey lhs, IBindingKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return string.Format("NameBindingKey:{0}", Name);
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