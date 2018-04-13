using UnityEngine;
using System;
using System.Collections.Generic;

namespace RadiacUI
{
    /// <summary>
    /// Only for text support.
    /// The localization support for other elements, such as textures, are managed out of the Radiac UI context.
    /// </summary>
    public static class LocalizationSupport
    {
        public static Dictionary<string, string> transfer = new Dictionary<string, string>();
        
        public static void LoadLocalizationFile()
        {
            // TODO!
            throw new NotImplementedException();
        }
        
        public static string GetLocalizedString(string indexer)
        {
            if(transfer.ContainsKey(indexer))
            {
                return transfer[indexer];
            }
            
            return indexer;
        }
    }
    
    
    
}
