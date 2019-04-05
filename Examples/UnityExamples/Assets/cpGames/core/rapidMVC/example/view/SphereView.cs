using System;
using UnityEngine;

namespace cpGames.core.RapidMVC.examples
{
    [Context("ExampleContext")]
    public class SphereView : ComponentView
    {
        #region Properties
        [Inject]
        public SphereModel Model { get; set; }
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            PropertyUpdatedSignal.AddListener(key =>
            {
                if (!Rapid.BindingKeyFactoryCollection.Create(typeof(SphereModel), out var modelKey, out var errorMessage))
                {
                    throw new Exception(errorMessage);
                }
                if (modelKey.Equals(key))
                {
                    GetComponent<Renderer>().material.SetColor("_Color", Model.color);
                }
            });
        }
        #endregion
    }
}