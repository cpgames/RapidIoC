namespace cpGames.core.RapidIoC.impl
{
    internal class AddressKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object? keyData, out IKey key)
        {
            key = Rapid.InvalidKey;
            if (keyData is Address address)
            {
                key = new AddressKey(address);
                return Outcome.Success();
            }
            return Outcome.Fail("keyData type is not supported.");
        }

        public bool CanCreate(object? keyData)
        {
            return keyData is Address;
        }
        #endregion
    }

    internal class AddressKey : IKey
    {
        #region Properties
        public Address Address { get; }
        #endregion

        #region Constructors
        public AddressKey(Address address)
        {
            Address = address;
        }
        #endregion

        #region Methods
        protected bool Equals(AddressKey other)
        {
            return Address == other.Address;
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
            return Equals((IdKey)obj);
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }

        public static bool operator ==(AddressKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(AddressKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"AddressKey:{Address}";
        }
        #endregion
    }
}