using UnityEngine;
using UnityEngine.UI;

namespace  RadiacUI
{
    [RequireComponent(typeof(Text))]
    public class RadiacText : RadiacUIComponent
    {
        public string requestString;
        
        Text text { get { return this.gameObject.GetComponent<Text>(); } }
        
        protected override void Start()
        {
            base.Start();
            
            if(requestString == "")
            {
                requestString = text.text;
            }
        }
        
        void SyncText()
        {
            text.text = LocalizationSupport.GetLocalizedString(requestString);
        }
        
        protected override void Update()
        {
            base.Update();
            
            // TODO...
            // Is is a good idea to get string every frame?
            SyncText();
        }
    }
}
