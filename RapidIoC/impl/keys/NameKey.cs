namespace cpGames.core.RapidIoC.impl
{
    internal class NameKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object? keyData, out IKey key)
        {
            key = Rapid.InvalidKey;
            if (keyData is string stringKeyData)
            {
                key = new NameKey(stringKeyData);
                return Outcome.Success();
            }
            return Outcome.Fail("keyData type is not supported.");
        }

        public bool CanCreate(object? keyData)
        {
            return keyData is string;
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
        private bool Equals(NameKey other)
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
            return Name.GetHashCode();
        }

        public static bool operator ==(NameKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(NameKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"NameKey:{Name}";
        }
        #endregion
    }
}