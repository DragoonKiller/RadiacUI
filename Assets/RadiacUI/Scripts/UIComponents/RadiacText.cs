using UnityEngine;
using UnityEngine.UI;

namespace  RadiacUI
{
    [RequireComponent(typeof(Text))]
    public class RadiacText : MonoBehaviour
    {
        public string requestString;
        
        Text tx;
        
        void SyncText()
        {
            tx.text = LocalizationSupport.GetLocalizedString(requestString);
        }
        
        void Start()
        {
            tx = this.gameObject.GetComponent<Text>();
        }
        
        void Update()
        {
            // TODO...
            // Is is a good idea to get string every frame?
            SyncText();
        }
    }
}
