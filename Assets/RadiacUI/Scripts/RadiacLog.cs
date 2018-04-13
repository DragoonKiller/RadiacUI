using UnityEngine;

namespace RadiacUI
{
    internal class Log
    {
        public static void AddLog(object s)
        {
            #if DEBUG
            Debug.Log(s);
            #endif
        }
        
        public static void AddLogFormat(string s, params object[] args)
        {
            #if DEBUG
            Debug.LogFormat(s, args);
            #endif
        }
        
        public static void AddWarning(object s)
        {
            Debug.LogWarning(s);
        }
        
        public static void AddWarningFormat(string s, params object[] args)
        {
            Debug.LogWarningFormat(s, args);
        }
    }
    
    
}
