using UnityEngine;
using UnityEditor;


public class ChoiceDesigner : EditorWindow
{
    private string myString = "Hello World";
    public ChoiceSlot target;

    [MenuItem("Window/ChoiceDesigner")]
    public static void ShowWindow()
    {
        var window = GetWindow<ChoiceDesigner>("Example");
    }

    private void OnGUI()
    {
        GUILayout.Label("This is bold lable");
        myString = EditorGUILayout.TextField("Name", myString);
    }
}
