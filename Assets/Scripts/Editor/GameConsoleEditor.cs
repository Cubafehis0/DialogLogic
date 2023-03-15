using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameConsole))]
public class GameConsoleEditor : Editor
{
    string cmd;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        cmd = EditorGUILayout.TextField("", cmd);

        if (GUILayout.Button("Execute"))
        {
            if (Application.isPlaying)
            {
                if (!string.IsNullOrEmpty(cmd))
                {
                    Debug.Log(((GameConsole)target).Execute(cmd));
                }
            }
        }
    }
}
