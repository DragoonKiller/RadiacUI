using UnityEngine;
using System;
using System.Collections.Generic;

namespace RadiacUI
{
    /// <summary>
    /// the Panel is the basic components that interacts with mouse input.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class RadiacPanel : RadiacUIComponent
    {
        internal static readonly HashSet<RadiacPanel> all = new HashSet<RadiacPanel>();
        
        public string[] signalMouseEnter;
        public string[] signalMouseLeave;
        public string[] signalMouseHover;
        public string[] signalMouseMove;
        
        Vector2 lastCursorPos;
        
        protected RadiacAuxiliaryArea[] aux { get { return this.gameObject.GetComponents<RadiacAuxiliaryRect>(); } }
        protected RectTransform tr { get { return this.gameObject.GetComponent<RectTransform>(); } }
        [SerializeField] bool useBaseRect = true;
        
        /// <summary>
        /// True IFF cursor is pointing to this object or at least one of this object's children.
        /// </summary>
        internal bool cursorHovering { get; private set; }
        
        protected override void Start()
        {
            base.Start();
            
            lastCursorPos = VirtualCursor.position;
            all.Add(this);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            all.Remove(this);
        }
        
        /// <summary>
        /// This stored value provides ability to measure the "Enter" event without moving cursor through the boundary.
        /// e.g. set a UIComponent to visiable while the cursor is in the territory of this UIComponent.
        /// We're not recording and using "mouse position in last frame" for this calculation.
        /// </summary>
        bool trigLast;
        
        protected override void Update()
        {
            base.Update();
            
            if(cursorHovering && !trigLast)
            {
                SignalManager.EmitSignal(signalMouseEnter);
            }
            
            if(!cursorHovering && trigLast)
            {
                SignalManager.EmitSignal(signalMouseLeave);
            }
            
            if(cursorHovering || trigLast)
            {
                SignalManager.EmitSignal(signalMouseHover);
                
                if(VirtualCursor.position != lastCursorPos)
                {
                    SignalManager.EmitSignal(signalMouseMove);
                }
            }
            
            trigLast = cursorHovering;
            lastCursorPos = VirtualCursor.position;
        }
        
        /// <summary>
        /// Test if a point is inside the panel's area.
        /// Notice this does not consider neither activity nor depth layout.
        /// </summary>
        internal bool IsPointInsidePanel(Vector2 pos)
        {
            bool x = false;
            if(useBaseRect) x |= tr.rect.Transform(tr.position).Contains(pos);
            if(aux != null) foreach(var i in aux) x |= i.IsPointInside(pos);
            return x;
        }
        
        public void OnDrawGizmosSelected()
        {
            if(useBaseRect) RadiacUtility.DrawRectangleGizmos(tr.rect.Transform(tr.position), tr.position.z + float.Epsilon);
        }
        
        // ============================================================================================================
        // ============================================================================================================
        // ============================================================================================================
        
        public static void InitUpdator()
        {
            if(listenerAssigned)
            {
                Debug.LogWarning("Radiac Panel's updator re-inited!");
            }
            
            RadiacEnvironment.RadiacUpdates += UpdateCursorHovering;
            listenerAssigned = true;
        }
        
        /// <summary>
        /// To build up a listener monitoring cursorHovering property, a static method will be assigned in Start().
        /// </summary>
        static bool listenerAssigned = false;
        static void UpdateCursorHovering()
        {
            // Find what the cursor hits.
            RadiacPanel res = null;
            foreach(var i in all)
            {
                if(i.active && i.IsPointInsidePanel(VirtualCursor.position))
                {
                    if(res == null || i.gameObject.transform.position.z < res.gameObject.transform.position.z)
                    {
                        res = i;
                    }
                }
            }
            
            // Set the hit and its parent objects "cursorHovering".
            foreach(var i in all) i.cursorHovering = false;
            
            while(res != null)
            {
                res.cursorHovering = true;
                res = res.gameObject.transform.parent.GetComponent<RadiacPanel>();
            }
        }
        
        
    }
}
