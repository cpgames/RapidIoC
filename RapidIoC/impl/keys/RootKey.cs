namespace cpGames.core.RapidIoC.impl
{
    internal class RootKey : Singleton<RootKey>, IKey
    {
        #region Constructors
        private RootKey() { }
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

        public static bool operator ==(RootKey lhs, IKey rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(RootKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return "RootKey";
        }
        #endregion
    }
}