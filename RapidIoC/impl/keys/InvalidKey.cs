namespace cpGames.core.RapidIoC.impl
{
    internal class InvalidKey : Singleton<InvalidKey>, IKey
    {
        #region Constructors
        private InvalidKey() { }
        #endregion

        #region Methods
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
            return obj.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(InvalidKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(InvalidKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return "InvalidKey";
        }
        #endregion
    }
}