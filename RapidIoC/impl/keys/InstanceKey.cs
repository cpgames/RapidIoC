namespace cpGames.core.RapidIoC.impl
{
    internal class InstanceKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object? keyData, out IKey key)
        {
            key = Rapid.InvalidKey;
            if (keyData != null)
            {
                var keyDataType = keyData.GetType();
                if (!keyDataType.IsValueType
                    && keyDataType != typeof(string))
                {
                    key = new InstanceKey(keyData);
                    return Outcome.Success();
                }
            }
            return Outcome.Fail("keyData type is not supported.", this);
        }
        #endregion
    }

    internal class InstanceKey : IKey
    {
        #region Properties
        public object Instance { get; }
        #endregion

        #region Constructors
        public InstanceKey(object instance)
        {
            Instance = instance;
        }
        #endregion

        #region Methods
        protected bool Equals(InstanceKey other)
        {
            return ReferenceEquals(Instance, other.Instance);
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
            return Equals((InstanceKey)obj);
        }

        public override int GetHashCode()
        {
            return Instance.GetHashCode();
        }

        public static bool operator ==(InstanceKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(InstanceKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"InstanceKey:{Instance}";
        }
        #endregion
    }
}