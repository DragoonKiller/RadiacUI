using UnityEngine;

namespace RadiacUI
{
    /// <summary>
    /// Auxiliary rect make responsers possible to react with non-rectangle area.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(RadiacUIComponent))]
    public class RadiacAuxiliaryRect : RadiacAuxiliaryArea
    {
        RectTransform tr;
        
        public virtual Rect rect
        {
            get
            {
                if(tr == null) tr = this.gameObject.GetComponent<RectTransform>();
                return _rect.Transform(tr.position);
            }
        }
        
        [SerializeField] Rect _rect;
        
        void Start()
        {
            tr = this.gameObject.GetComponent<RectTransform>();
        }
        
        public override bool IsPointInside(Vector2 point)
            => rect.Contains(point);
        
        public void OnDrawGizmosSelected()
            => RadiacUtility.DrawRectangleGizmos(rect, tr.position.z + float.Epsilon);
        
    }
    
}
