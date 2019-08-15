using System;
using System.Collections.Generic;

namespace cpGames.core
{
    public class UidGenerator
    {
        #region Fields
        private static readonly Random _rnd = new Random();
        private readonly List<long> _uids = new List<long>();
        #endregion

        #region Methods
        public long GenerateUid()
        {
            long uid;
            do
            {
                uid = (long)(_rnd.NextDouble() * (long.MaxValue - 1)) + 1;
            } while (_uids.Contains(uid));
            _uids.Add(uid);

            return uid;
        }

        public void RemoveUid(long uid)
        {
            _uids.Remove(uid);
        }

        public void AddUid(long uid)
        {
            if (_uids.Contains(uid))
            {
                throw new Exception("Uid already exists");
            }
            _uids.Add(uid);
        }

        public void Clear()
        {
            _uids.Clear();
        }

        public bool HasUid(long uid)
        {
            return _uids.Contains(uid);
        }
        #endregion
    }
}