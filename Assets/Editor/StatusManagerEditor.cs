using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatusManagerPacked))]
public class StatusManagerEditor : Editor
{
    //–Ú¡–ªØ
    private SerializedObject test;
    public override void OnInspectorGUI()
    {
        //test = new SerializedObject(target);

        
        base.OnInspectorGUI();
        if (GUILayout.Button("test"))
        {
            Debug.Log("123");
        }
        //EditorGUILayout.Toggle(true);
    }
}
