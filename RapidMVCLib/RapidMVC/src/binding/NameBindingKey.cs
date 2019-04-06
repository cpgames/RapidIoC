using System;

namespace cpGames.core.RapidMVC.src
{
    internal class NameBindingKeyFactory : IBindingKeyFactory
    {
        #region IBindingKeyFactory Members
        public bool Create(object keyData, out IBindingKey key, out string errorMessage)
        {
            key = null;
            if (keyData is string stringKeyData)
            {
                try
                {
                    key = new NameBindingKey(stringKeyData);
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

    internal class NameBindingKey : IBindingKey
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
}