using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace RadiacUI
{
    [DisallowMultipleComponent]
    public abstract class RadiacUIComponent : SignalReceiver
    {
        public bool selfActive;
        public bool active { get { return selfActive && (parent == null || parent.active); } }
        
        Image image;
        
        public Color baseColor = Color.white;
        
        [Range(0, 2)] public float fadeTime = 1.0f;
        
        RadiacUIComponent parent = null;
        
        [SerializeField] string switchSignal = "";
        [SerializeField] string activeSignal = "";
        [SerializeField] string deactiveSignal = "";
        
        protected virtual void Start()
        {
            if(switchSignal != "") AddCallback(new Signal(switchSignal), () => selfActive = !selfActive);
            if(activeSignal != "") AddCallback(new Signal(activeSignal), () => selfActive = true);
            if(deactiveSignal != "") AddCallback(new Signal(deactiveSignal), () => selfActive = false);
            
            image = this.gameObject.GetComponent<Image>();
            if(fadeTime == 0f) throw new ArgumentOutOfRangeException();
            
            image.color = baseColor;
            
            var par = this.gameObject.transform.parent.gameObject;
            if(par.GetComponent<Canvas>() == null)
            {
                parent = par.GetComponent<RadiacUIComponent>();
                if(parent == null)
                {
                    throw new Exception("A UI Window must be attached to Canvas directly or has a parent mounted with UI Window.");
                }
            }
        }
        
        protected virtual void Update()
        {
            float step = baseColor.a / fadeTime * Time.deltaTime;
            float a = Mathf.Clamp(image.color.a + (active ? 1 : -1) * step, 0f, 1.0f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        }
        
    }
    
    
}
