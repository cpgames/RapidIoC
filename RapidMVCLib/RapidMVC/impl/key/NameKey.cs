namespace cpGames.core.RapidMVC.impl
{
    internal class NameKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public bool Create(object keyData, out IKey key, out string errorMessage)
        {
            key = null;
            errorMessage = string.Empty;
            if (keyData is string stringKeyData)
            {
                key = new NameKey(stringKeyData);
                return true;
            }
            errorMessage = "keyData type is not supported.";
            return false;
        }
        #endregion
    }

    internal class NameKey : IKey
    {
        #region Properties
        public string Name { get; }
        #endregion

        #region Constructors
        public NameKey(string name)
        {
            Name = name;
        }
        #endregion

        #region Methods
        protected bool Equals(NameKey other)
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
            return Equals((NameKey)obj);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public static bool operator ==(NameKey lhs, IKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(NameKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return string.Format("NameKey:{0}", Name);
        }
        #endregion
    }
}