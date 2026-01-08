using System;

namespace cpGames.core.RapidIoC
{
    public static class StateContextMethods
    {
        #region Methods
        public static T SetState<T>(IContextBase context) where T : IStateBase
        {
            var state = (T)Activator.CreateInstance(typeof(T));
            state.SetContext(context);
            return state;
        }
        #endregion
    }
}