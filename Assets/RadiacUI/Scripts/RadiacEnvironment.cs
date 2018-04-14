using UnityEngine;
using System;

namespace RadiacUI
{
    /// <summary>
    /// This class provides a global Update and FixedUpdate used by Radaic UI system.
    /// In theory this class can be mounted on any GameObject that will never be destroyed.
    /// </summary>
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
        }
        
        void Update()
        {
            RadiacUpdates();
        }
        
        void FixedUpdate()
        {
            RadiacFixedUpdates();
        }
        
        ~RadiacEnvironment()
        {
            RadiacUpdates = () => { };
            RadiacFixedUpdates = () => { };
        }
    }
}
