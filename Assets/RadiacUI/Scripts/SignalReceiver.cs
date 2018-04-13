using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace RadiacUI
{
    [RequireComponent(typeof(RectTransform))]
    public class SignalReceiver : MonoBehaviour
    {
        public readonly Dictionary<Signal, Action> actionList = new Dictionary<Signal, Action>();
        
        protected void AddCallback(Signal x, Action action)
        {
            actionList.Add(x, action);
            SignalManager.AddSignalCallback(x, action);
        }
        
        void OnDestroy()
        {
            foreach(var i in actionList) SignalManager.RemoveSignalCallback(i.Key, i.Value);
        }
    }
}
