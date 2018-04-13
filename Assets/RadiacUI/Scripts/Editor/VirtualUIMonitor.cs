using UnityEditor;
using UnityEngine;

namespace RadiacUI
{
    public class VirtualUIMonitor : EditorWindow
    {
        [MenuItem("RadiacUI/Open Virtual UI Monitor")]
        static void ShowWindow()
        {
            EditorWindow.GetWindow<VirtualUIMonitor>("Virtual Cursor");
        }
        
        public void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Camera");
            EditorGUILayout.Vector2Field("Size", VirtualCamera.size);
            EditorGUILayout.Vector3Field("Positon", VirtualCamera.position);
            EditorGUILayout.Vector3Field("Direction", VirtualCamera.direction);
            GUILayout.EndVertical();
            
            GUILayout.BeginVertical();
            GUILayout.Label("Cursor");
            EditorGUILayout.Vector2Field("Position", VirtualCursor.position);
            EditorGUILayout.Vector2Field("Delta Position", VirtualCursor.deltaPosition);
            EditorGUILayout.Vector2Field("Viewport Position", VirtualCursor.viewportPosition);
            EditorGUILayout.EndVertical();
            Repaint();
        }
    }
}
