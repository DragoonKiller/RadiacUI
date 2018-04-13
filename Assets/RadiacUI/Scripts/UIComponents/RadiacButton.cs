using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace RadiacUI
{
    [RequireComponent(typeof(RectTransform))]
    public class RadiacButton : RadiacPanel
    {
        public string[] signalMouseClick;
        public string[] signalMousePressing;
        public string[] signalMouseRelease;
        
        protected override void Update()
        {
            base.Update();
            
            if(curTriggered)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    SignalManager.EmitSignal(signalMouseClick);
                }
                
                if(Input.GetMouseButton(0))
                {
                    SignalManager.EmitSignal(signalMousePressing);
                }
                
                if(Input.GetMouseButtonUp(0))
                {
                    SignalManager.EmitSignal(signalMouseRelease);
                }
            }
            
        }
    }
    
    
}
