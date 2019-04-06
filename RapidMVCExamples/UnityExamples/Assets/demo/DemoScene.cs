using cpGames.core.RapidMVC;
using UnityEngine;
public class DemoScene : SceneView
{
    #region Fields
    public float maxSize = 5.0f;
    public float sizeGrowth = 0.1f;
    public float size = 10.0f;
    #endregion

    #region Methods
    private void Update()
    {
        size = (size + Time.deltaTime * sizeGrowth) % maxSize;
        Rapid.Bind("Size", size, "DemoContext");
    }
    #endregion
}