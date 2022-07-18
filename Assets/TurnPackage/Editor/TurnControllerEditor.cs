using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TurnController))]
public class TurnControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var t = (TurnController)target;
        if (GUILayout.Button("½áÊø»ØºÏ"))
        {
            t.EndTurn();
        }
    }
}
