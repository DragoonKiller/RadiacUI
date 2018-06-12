using UnityEngine;
using System;
using System.Collections.Generic;

namespace RadiacUI
{
    // ================================================================================================================
    // ================================================================================================================
    // ================================================================================================================
    
    public static class RadiacFunctional
    {
        public static To[] Map<From, To>(From[] src, Func<From, To> f)
        {
            To[] res = new To[src.Length];
            for(int i=0; i<=src.Length; i++) res[i] = f(src[i]); 
            return res;
        }
        
        public static SetTo Map<From, To, SetFrom, SetTo>(IEnumerable<From> src, Func<From, To> f)
            where SetTo : ICollection<To>, new()
        {
            SetTo res = new SetTo();
            foreach(var i in src) res.Add(f(i));
            return res;
        }
        
        public static int Count<T>(IEnumerable<T> src, Predicate<T> f)
        {
            int c = 0;
            foreach(var i in src) if(f(i)) c++;
            return c;
        }
        
        public static RetType Filter<T, RetType>(IEnumerable<T> src, Predicate<T> f)
            where RetType : ICollection<T>, new()
        {
            RetType res = new RetType();
            foreach(var i in src) res.Add(i);
            return res;
        }
        
        public static Target ToCollection<T, Target>(this IEnumerable<T> src)
            where Target : ICollection<T>, new()
        {
            Target res = new Target();
            foreach(var i in src) res.Add(i);
            return res;
        }
        
        public static List<T> ToList<T>(this IEnumerable<T> src)
        {
            return src.ToCollection<T, List<T>>();
        }
        
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> src)
        {
            return src.ToCollection<T, HashSet<T>>();
        }
    }
    
    // ================================================================================================================
    // ================================================================================================================
    // ================================================================================================================
    
    internal static class RadiacCollection
    {
        public static V ElementAt<K, V>(this IDictionary<K, V> dict, K index)
            where V : new()
        {
            if(dict.ContainsKey(index)) return dict[index];
            V newElement = new V();
            dict.Add(index, newElement);
            return newElement;
        }
    }
    
    // ================================================================================================================
    // ================================================================================================================
    // ================================================================================================================
    
    internal static class RadiacAlgorithm
    {
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
    }
    
    // ================================================================================================================
    // ================================================================================================================
    // ================================================================================================================
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
