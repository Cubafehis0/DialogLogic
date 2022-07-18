using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AbilityModule))]
public class AbilityGroupEditor : Editor
{
    string abilityName = "Weak";
    int cnt = 1;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AbilityModule t = (AbilityModule)target;
        abilityName = EditorGUILayout.TextField("ability", abilityName);
        cnt = EditorGUILayout.IntField("cnt", cnt);
        if (GUILayout.Button("add"))
        {
            t.AddAbility(abilityName, cnt);
        }
    }
}
