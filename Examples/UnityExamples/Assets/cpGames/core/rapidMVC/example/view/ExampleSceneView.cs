using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cpGames.core.RapidMVC.examples
{
    [Context("ExampleContext")]
    public class ExampleSceneView : SceneView
    {
        #region Fields
        private int _index;
        public List<SphereModel> models;
        #endregion

        #region Methods
        protected override void MapBindings()
        {
            if (models == null || models.Count == 0)
            {
                throw new Exception("Model collection is empty.");
            }
            Rapid.Bind(typeof(SphereModel), models[_index]);
            StartCoroutine(UpdateModel());
        }

        private IEnumerator UpdateModel()
        {
            yield return new WaitForSeconds(1);
            _index = (_index + 1) % models.Count;
            Rapid.Bind(typeof(SphereModel), models[_index]);
            yield return StartCoroutine(UpdateModel());
        }
        #endregion
    }
}