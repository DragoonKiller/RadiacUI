using UnityEngine;
using UnityEngine.UI;

using System;

namespace RadiacUI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RadiacUIComponent))]
    [DisallowMultipleComponent]
    public sealed class RadiacUIImage : MonoBehaviour
    {
        RadiacUIComponent uiBase;
        Image image;
        
        public Color baseColor = Color.white;
        
        [Range(0, 2)] public float fadeTime = 1.0f;
        
        void Start()
        {
            image = this.gameObject.GetComponent<Image>();
            if(fadeTime == 0f) throw new ArgumentOutOfRangeException();
            image.color = baseColor;
            
            uiBase = this.gameObject.GetComponent<RadiacUIComponent>();
        }
        
        void Update()
        {
            float step = baseColor.a / fadeTime * Time.deltaTime;
            float a = Mathf.Clamp(image.color.a + (uiBase.active ? 1 : -1) * step, 0f, 1.0f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        }
    }
    
}
