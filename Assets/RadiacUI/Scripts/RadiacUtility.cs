using UnityEngine;
using System;
using System.Collections.Generic;

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
        
        internal static bool Contains<T>(this IList<T> v, T val) where T : IEquatable<T>
        {
            foreach(var i in v) if(i.Equals(val)) return true;
            return false;
        }
        
        internal enum DFSStyle
        {
            Preorder,
            Postorder
        }
        
        internal static void ForeachChild(Transform t, Action<Transform> f)
        {
            for(int i=0; i<t.childCount; i++) f(t.GetChild(i));
        }
        
        internal static void DFSHierarchy(Transform root, Action<Transform> f, DFSStyle style = DFSStyle.Preorder)
        {
            switch(style)
            {
                case DFSStyle.Preorder :
                    DFSPreorder(root, f);
                break;
                case DFSStyle.Postorder :
                    DFSPostorder(root, f);
                break;
                default : throw new ArgumentOutOfRangeException();
            }
        }
        
        static void DFSPreorder(Transform root, Action<Transform> f)
        {
            f(root);
            ForeachChild(root, (x) => DFSPreorder(x, f));
        }
        
        static void DFSPostorder(Transform root, Action<Transform> f)
        {
            ForeachChild(root, (x) => DFSPreorder(x, f));
            f(root);
        }
        
        internal static void DrawRectangleGizmos(Rect rect, float h)
        {
            var bottomLeft = new Vector3(rect.xMin, rect.yMin, h);
            var bottomRight = new Vector3(rect.xMax, rect.yMin, h);
            var topLeft = new Vector3(rect.xMin, rect.yMax, h);
            var topRight = new Vector3(rect.xMax, rect.yMax, h);
            Debug.DrawLine(bottomLeft, bottomRight, Color.red);
            Debug.DrawLine(topLeft, topRight, Color.red);
            Debug.DrawLine(bottomLeft, topLeft, Color.red);
            Debug.DrawLine(bottomRight, topRight, Color.red);
        }
        
        
    }
    
    
    
}
