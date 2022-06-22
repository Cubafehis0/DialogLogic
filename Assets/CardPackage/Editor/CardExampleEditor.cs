using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardExample))]
public class CardExampleEditor : Editor
{
    //test = new SerializedObject(target);
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var t = (CardExample)target;
        if (GUILayout.Button("Draw"))
        {
            t.cardController.Draw(1);
        }
    }

}
