namespace cpGames.core.RapidIoC.impl
{
    internal class ByteKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public Outcome Create(object keyData, out IKey? key)
        {
            key = null;
            if (keyData is byte byteKeyData)
            {
                key = new ByteKey(byteKeyData);
                return Outcome.Success();
            }
            return Outcome.Fail("keyData type is not supported.");
        }
        #endregion
    }

    internal class ByteKey : IKey
    {
        #region Properties
        public byte Data { get; }
        #endregion

        #region Constructors
        public ByteKey(byte data)
        {
            Data = data;
        }
        #endregion

        #region Methods
        protected bool Equals(ByteKey other)
        {
            return Data == other.Data;
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
            return Equals((ByteKey)obj);
        }

        public override int GetHashCode()
        {
            return Data.GetHashCode();
        }

        public static bool operator ==(ByteKey lhs, IKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(ByteKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"ByteKey:{Data}";
        }
        #endregion
    }
}