using UnityEngine;

namespace cpGames.core.RapidMVC
{
    public class ComponentView : MonoBehaviour, IView
    {
        #region IView Members
        public Signal<IBindingKey> PropertyUpdatedSignal { get; } = new Signal<IBindingKey>();
        #endregion

        #region Methods
        protected virtual void Awake()
        {
            Rapid.RegisterView(this);
            MapBindings();
        }

        protected virtual void MapBindings() { }
        #endregion
    }
}