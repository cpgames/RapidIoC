namespace cpGames.core.RapidIoC.impl
{
    internal class InstanceKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public bool Create(object keyData, out IKey key, out string errorMessage)
        {
            key = null;
            errorMessage = string.Empty;
            var keyDataType = keyData.GetType();
            if (!keyDataType.IsValueType 
                && keyDataType != typeof(string))
            {
                key = new InstanceKey(keyData);
                return true;
            }
            errorMessage = "keyData type is not supported.";
            return false;
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
            return Instance != null ? Instance.GetHashCode() : 0;
        }

        public static bool operator ==(InstanceKey lhs, IKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(InstanceKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return string.Format("InstanceKey:{0}", Instance);
        }
        #endregion
    }
}