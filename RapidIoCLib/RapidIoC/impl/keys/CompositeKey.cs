using System.Collections.Generic;
using System.Linq;
using zLib;

namespace cpGames.core.RapidIoC.impl
{
    internal class CompositeKeyFactory : IKeyFactory
    {
        #region IKeyFactory Members
        public bool Create(object keyData, out IKey key)
        {
            key = null;
            if (keyData is List<object> childKeyDatas)
            {
                var keyList = new List<IKey>();
                foreach (var childKeyData in childKeyDatas)
                {
                    if (!Rapid.KeyFactoryCollection.Create(childKeyData, out var childKey))
                    {
                        return false;
                    }
                    keyList.Add(childKey);
                }
                key = new CompositeKey(keyList);
                return true;
            }
            return false;
        }

        public bool Create(object keyData, out IKey key, out string errorMessage)
        {
            key = null;
            errorMessage = string.Empty;
            if (keyData is List<object> childKeyDatas)
            {
                var keyList = new List<IKey>();
                foreach (var childKeyData in childKeyDatas)
                {
                    if (!Rapid.KeyFactoryCollection.Create(childKeyData, out var childKey, out errorMessage))
                    {
                        return false;
                    }
                    keyList.Add(childKey);
                }
                key = new CompositeKey(keyList);
                return true;
            }
            errorMessage = "keyData type is not supported.";
            return false;
        }
        #endregion
    }

    internal class CompositeKey : IKey
    {
        #region Properties
        public List<IKey> Keys { get; }
        #endregion

        #region Constructors
        public CompositeKey(List<IKey> keys)
        {
            Keys = keys;
        }
        #endregion

        #region Methods
        protected bool Equals(CompositeKey other)
        {
            return
                other.Keys.Count == Keys.Count &&
                !other.Keys.Where((t, i) => !t.Equals(Keys[i])).Any();
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
            return Equals((CompositeKey)obj);
        }

        public override int GetHashCode()
        {
            return
                Keys == null ? 0 : Keys.Aggregate(0, (current, key) => current ^ key.GetHashCode());
        }

        public static bool operator ==(CompositeKey lhs, IKey rhs)
        {
            return lhs?.Equals(rhs) ?? ReferenceEquals(rhs, null);
        }

        public static bool operator !=(CompositeKey lhs, IKey rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return $"CompositeKey:{Keys.ToString(",")}";
        }
        #endregion
    }
}