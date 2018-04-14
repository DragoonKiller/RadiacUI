using UnityEngine;

namespace RadiacUI
{
    /// <summary>
    /// Auxiliary rect make responsers possible to react with non-rectangle area.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(RadiacUIComponent))]
    public abstract class RadiacAuxiliaryArea : MonoBehaviour
    {
        public abstract bool IsPointInside(Vector2 point);
    }
    
}
