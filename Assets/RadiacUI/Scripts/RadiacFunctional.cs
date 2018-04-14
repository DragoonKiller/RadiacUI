using System;
using System.Collections.Generic;

namespace RadiacUI
{
    public static class Functional
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
    
}
