using UnityEngine;
using System;

namespace RadiacUI
{
    [DisallowMultipleComponent]
    public class RadiacEnvironment : MonoBehaviour
    {
        public static RadiacEnvironment instance;
        public static Action RadiacUpdates;
        public static Action RadiacFixedUpdates;
        
        void Start()
        {
            if(instance != null) Debug.LogWarning("Radiac Environment replaced.");
            
            instance = this;
            RadiacUpdates = () => { };
            RadiacFixedUpdates = () => { };
        }
        
        void Update()
        {
            RadiacUpdates();
        }
        
        void FixedUpdate()
        {
            RadiacFixedUpdates();
        }
    }
}
