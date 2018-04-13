using UnityEngine;
using System;

namespace RadiacUI
{
    internal static class RadiacUtility
    {
        internal static Rect Transform(this Rect rect, Vector2 pos)
        {
            return new Rect(rect.x + pos.x, rect.y + pos.y, rect.width, rect.height);
        }
        
        internal static bool IsInRect(this Vector2 pos, Rect rect)
        {
            return rect.Contains(pos);
        }
        
        internal static bool IsInAnyOf(this Vector2 pos, params Rect[] rects)
        {
            foreach(var i in rects) if(pos.IsInRect(i)) return true;
            return false;
        }
        
        internal static bool IsInAllOf(this Vector2 pos, params Rect[] rects)
        {
            foreach(var i in rects) if(!pos.IsInRect(i)) return false;
            return true;
        }
    }
    
    
    
}
