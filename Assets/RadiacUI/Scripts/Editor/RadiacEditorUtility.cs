using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;


namespace RadiacUI
{
    internal static class RadiacEditorUtility
    {
        public static List<Type> GetClassesInNamespace(Assembly asm, string namespaceName)
        {
            return Functional.Filter<Type, List<Type>>(asm.GetTypes(), (i) => i.Namespace == namespaceName);
        }
        
        public static List<Type> GetStaticClasses(List<Type> list)
        {
            return Functional.Filter<Type, List<Type>>(list, (i) => i.IsAbstract && i.IsSealed);
        }
        
        public static List<FieldInfo> GetStaticFields(Type type)
        {
            return Functional.Filter<FieldInfo, List<FieldInfo>>(
                type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                (i) => i.IsStatic);
        }
    }
    
    
    
}
