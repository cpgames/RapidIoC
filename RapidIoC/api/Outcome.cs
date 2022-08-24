namespace cpGames.core.RapidIoC
{
    public class Outcome
    {
        #region Fields
        private static readonly Outcome EMPTY_FAIL = new Outcome(false, string.Empty);
        private static readonly Outcome EMPTY_SUCCESS = new Outcome(true, string.Empty);
        #endregion

        #region Properties
        public bool IsSuccess { get;  }
        public string ErrorMessage { get;  }
        #endregion

        #region Constructors
        private Outcome(bool success, string errorMessage)
        {
            IsSuccess = success;
            ErrorMessage = errorMessage;
        }
        #endregion

        #region Methods
        protected bool Equals(Outcome other)
        {
            return IsSuccess == other.IsSuccess;
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
            return Equals((Outcome)obj);
        }

        public override int GetHashCode()
        {
            return IsSuccess.GetHashCode();
        }

        public static implicit operator bool(Outcome r)
        {
            return r.IsSuccess;
        }

        public static Outcome operator &(Outcome a, Outcome b)
        {
            return !a.IsSuccess ? a : b;
        }

        public static Outcome operator |(Outcome a, Outcome b)
        {
            if (a.IsSuccess)
            {
                return a;
            }
            if (b.IsSuccess)
            {
                return b;
            }
            return Fail($"{a.ErrorMessage}\n{b.ErrorMessage}");
        }

        public static bool operator ==(Outcome a, Outcome b)
        {
            return a.IsSuccess == b.IsSuccess;
        }

        public static bool operator !=(Outcome a, Outcome b)
        {
            return !(a == b);
        }

        public static bool operator false(Outcome a)
        {
            return !a.IsSuccess;
        }

        public static bool operator true(Outcome a)
        {
            return a.IsSuccess;
        }

        public override string ToString()
        {
            return ErrorMessage;
        }

        public static Outcome Success()
        {
            return EMPTY_SUCCESS;
        }

        public static Outcome Fail(string errorMessage)
        {
            return new Outcome(false, errorMessage);
        }
        public static Outcome Fail()
        {
            return EMPTY_FAIL;
        }
        #endregion
    }
}