using UnityEngine;
using System;

namespace RadiacUI
{
    [RequireComponent(typeof(RectTransform))]
    public class RadiacPanel : RadiacUIComponent
    {
        public string[] signalMouseEnter;
        public string[] signalMouseLeave;
        public string[] signalMouseHover;
        public string[] signalMouseMove;
        
        Vector2 lastCursorPos;
        
        protected RadiacAuxiliaryRect[] aux;
        protected RectTransform tr;
        protected override void Start()
        {
            base.Start();
            
            tr = this.gameObject.GetComponent<RectTransform>();
            lastCursorPos = VirtualCursor.position;
            aux = this.gameObject.GetComponents<RadiacAuxiliaryRect>();
        }
        
        /// <summary>
        /// This property is calculated wherever the cursor is, but to do with visiability.
        /// This is necessary since we need to check if the cursor is leaving and if is sheltered from another UIComponent.
        /// </summary>
        protected bool curTriggered
        {
             get
             {
                 if(!active) return false;
                bool curTrig = false;
                curTrig |= tr.rect.Transform(tr.position).Contains(VirtualCursor.position);
                if(aux != null) foreach(var i in aux) curTrig |= i.rect.Contains(VirtualCursor.position);
                return curTrig;
             }
        }
        
        /// <summary>
        /// This stored value provides ability to measure the "Enter" event without moving cursor through the boundary.
        /// e.g. set a UIComponent to visiable while the cursor is in the territory of this UIComponent.
        /// We're not recording and using "mouse position in last frame" for this calculation.
        /// </summary>
        bool lastTriggered;
        
        protected override void Update()
        {
            base.Update();
            
            if(curTriggered && !lastTriggered)
            {
                SignalManager.EmitSignal(signalMouseEnter);
            }
            
            if(!curTriggered && lastTriggered)
            {
                SignalManager.EmitSignal(signalMouseLeave);
            }
            
            if(curTriggered || lastTriggered)
            {
                SignalManager.EmitSignal(signalMouseHover);
                
                if(VirtualCursor.position != lastCursorPos)
                {
                    SignalManager.EmitSignal(signalMouseMove);
                }
            }
            
            lastTriggered = curTriggered;
            lastCursorPos = VirtualCursor.position;
        }
    }
}
