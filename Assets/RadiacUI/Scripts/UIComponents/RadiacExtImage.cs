using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

namespace RadiacUI
{
    public class RadiacExtImage : RawImage
    {
        protected override void Awake()
        {
            base.Awake();
            RadiacExtImageAuxiliary.extImages.Add(this);
            material = new Material(Shader.Find("RadiacUI/ExtendableWidget")); 
        }
        
        protected override void OnDestroy()
        {
            RadiacExtImageAuxiliary.extImages.Remove(this);
        }
        
        /// <summary>
        /// Runs in game play.
        /// Not run in editor.
        /// </summary>
        protected void Update()
        {
            CheckScale();
            UpdateParameters();
        }
        
        public void UpdateParameters()
        {
            Vector4 drawRect = new Vector4(
                rectTransform.position.x + rectTransform.rect.xMin,
                rectTransform.position.y + rectTransform.rect.yMin,
                rectTransform.rect.width,
                rectTransform.rect.height);
            material.SetVector("_DrawRect", drawRect);
            material.SetVector("_ScreenSize", new Vector2(Screen.width, Screen.height));
        }
        
        bool CheckScale()
        {
            if(new Vector2(rectTransform.localScale.x, rectTransform.localScale.y) != Vector2.one)
            {
                Log.AddWarning("Non-Identity scale applied to the ExtImage is not fit and may cause bugs. Set it back to (1, 1, 1).");
                return false;
            }
            return true;
        }
    }
    
    [InitializeOnLoad]
    internal static class RadiacExtImageAuxiliary
    {
        public static HashSet<RadiacExtImage> extImages = new HashSet<RadiacExtImage>();
        
        static RadiacExtImageAuxiliary()
        {
            EditorApplication.update += OnRenderObject;
        }
        
        /// <summary>
        /// Runs in editor.
        /// Not run in game play.
        /// </summary>
        static void OnRenderObject()
        {
            foreach(var i in extImages)
            {
                i.UpdateParameters();
            }
        }
    }
    
}
