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
        #endregion
    }
}