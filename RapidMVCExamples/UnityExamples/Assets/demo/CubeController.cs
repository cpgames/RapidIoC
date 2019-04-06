using cpGames.core.RapidMVC;
using UnityEngine;
[Context("DemoContext")]
public class CubeController : ComponentView
{
    #region Fields
    private float _size;
    #endregion

    #region Properties
    [Inject("Size")]
    public float Size
    {
        get => _size;
        set
        {
            _size = value;
            transform.localScale = Vector3.one * _size;
        }
    }
    #endregion
}