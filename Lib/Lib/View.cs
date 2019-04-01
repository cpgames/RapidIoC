using System;
using cpGames.core.Serialization;

namespace cpGames.core.RapidMVC
{
    public interface IView { }

    public static class ViewHelpers
    {
        #region Methods
        public static void RegisterWithContext(this IView view)
        {
            var att = view.GetType().GetAttribute<ContextAttribute>();
            if (att == null)
            {
                throw new Exception(string.Format("View <{0}> missing context attribute", view));
            }
            var context = GlobalContext.Instance.FindContext(att.Name);
            context.RegisterView(view);
        }
        #endregion
    }
}