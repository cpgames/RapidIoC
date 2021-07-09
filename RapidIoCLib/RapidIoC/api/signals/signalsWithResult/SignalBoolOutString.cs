using System;

namespace cpGames.core.RapidIoC
{
    public class SignalBoolOutString : SignalBoolOut<string>
    {
        #region Properties
        public override bool DefaultResult => true;
        public override bool StopOnResult => true;
        public override bool TargetResult => false;
        #endregion

        #region Methods
        public override bool ResultEquals(bool a, bool b)
        {
            return a == b;
        }

        public override bool ResultAggregate(bool a, bool b)
        {
            return a && b;
        }

        public IKey AddCommand(Action callback, object keyData = null, bool once = false)
        {
            return AddCommand((out string str) =>
            {
                callback();
                str = string.Empty;
                return true;
            }, keyData, once);
        }
        #endregion
    }

    public class SignalBoolOutString<T_In> : SignalResultOut<bool, T_In, string>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        public override bool ResultEquals(bool a, bool b)
        {
            return a == b;
        }

        public override bool ResultAggregate(bool a, bool b)
        {
            return a && b;
        }

        public IKey AddCommand(Action<T_In> callback, object keyData = null, bool once = false)
        {
            return AddCommand((T_In @in, out string str) =>
            {
                callback(@in);
                str = string.Empty;
                return true;
            }, keyData, once);
        }
        #endregion
    }

    public class SignalBoolOutString<T_In_1, T_In_2> : SignalResultOut<bool, T_In_1, T_In_2, string>
    {
        #region Properties
        public override bool DefaultResult => true;
        #endregion

        #region Methods
        public override bool ResultEquals(bool a, bool b)
        {
            return a == b;
        }

        public override bool ResultAggregate(bool a, bool b)
        {
            return a && b;
        }

        public IKey AddCommand(Action<T_In_1, T_In_2> callback, object keyData = null, bool once = false)
        {
            return AddCommand((T_In_1 in1, T_In_2 in2, out string str) =>
            {
                callback(in1, in2);
                str = string.Empty;
                return true;
            }, keyData, once);
        }
        #endregion
    }
}