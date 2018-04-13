using UnityEngine;

namespace RadiacUI
{
    public sealed class VirtualCursor : MonoBehaviour
    {
        // In a Time.deltaTime, which Update() takes.
        public static Vector2 deltaPosition { get; private set; }
        
        // In pixels.
        public static Vector2 position
        {
            get
            {
                return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
        
        // In [-1, 1].
        public static Vector2 viewportPosition
        {
            get
            {
                return new Vector2(position.x * 2f / VirtualCamera.size.x - 1f, position.y * 2f / VirtualCamera.size.y - 1f);
            }
        }
        
        public static Ray ray
        {
            get
            {
                return VirtualCamera.bindedCamera.ScreenPointToRay(new Vector2(position.x, position.y));
            }
        }
    }
    
}
