using UnityEngine;

namespace RadiacUI
{
    /// <summary>
    /// Auxiliary rect make responsers possible to react with non-rectangle area.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(RadiacUIComponent))]
    public class RadiacAuxiliaryRect : MonoBehaviour
    {
        RectTransform tr;
        
        public virtual Rect rect
        {
            get
            {
                return _rect.Transform(tr.position);
            }
        }
        
        [SerializeField] Rect _rect;
        
        void Start()
        {
            tr = this.gameObject.GetComponent<RectTransform>();
        }
        
        public void OnDrawGizmosSelected()
        {
            tr = this.gameObject.GetComponent<RectTransform>();
            var bottomLeft = new Vector2(rect.xMin, rect.yMin);
            var bottomRight = new Vector2(rect.xMax, rect.yMin);
            var topLeft = new Vector2(rect.xMin, rect.yMax);
            var topRight = new Vector2(rect.xMax, rect.yMax);
            Debug.DrawLine(bottomLeft, bottomRight, Color.red);
            Debug.DrawLine(topLeft, topRight, Color.red);
            Debug.DrawLine(bottomLeft, topLeft, Color.red);
            Debug.DrawLine(bottomRight, topRight, Color.red);
        }
    }
    
}
