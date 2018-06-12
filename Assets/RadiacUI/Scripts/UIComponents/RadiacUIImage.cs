using UnityEngine;
using UnityEngine.UI;

using System;

namespace RadiacUI
{
    [RequireComponent(typeof(Graphic))]
    [RequireComponent(typeof(RadiacUIComponent))]
    [DisallowMultipleComponent]
    public sealed class RadiacUIImage : MonoBehaviour
    {
        public Color baseColor = Color.white;
        public float fadeSpeed;
        
        Graphic image { get { return this.gameObject.GetComponent<Graphic>(); } }
        RadiacUIComponent uiBase { get { return this.gameObject.GetComponent<RadiacUIComponent>(); } }
        RadiacUIImage parent { get { return this.gameObject.transform.parent.gameObject.GetComponent<RadiacUIImage>(); } }
        
        public float selfTransparency;
        public float transparency { get { return (parent == null ? 1.0f : parent.transparency) * selfTransparency;  } }
        
        void Start()
        {
            if(fadeSpeed <= 0f) throw new ArgumentOutOfRangeException();
        }
        
        void Update()
        {
            float step = fadeSpeed * Time.deltaTime;
            selfTransparency = Mathf.Clamp(selfTransparency + (uiBase.active ? 1 : -1) * step, 0f, 1.0f);
            image.color = new Color(image.color.r, image.color.g, image.color.b, transparency);
        }
    }
}
