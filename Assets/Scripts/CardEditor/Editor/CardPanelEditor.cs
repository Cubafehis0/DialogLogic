using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace CardEditor
{
    [CustomEditor(typeof(CardPanel))]
    [CanEditMultipleObjects]
    public class CardPanelEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            CardPanel myScript = (CardPanel)target;
            if (GUILayout.Button("ˢ�½���"))
            {
                myScript.RefreshPage();
            }
            if (GUILayout.Button("�������"))
            {
                myScript.DestroyAllChildren();
            }
        }
    }
    //public class CardPsanelEditor
    //{
    //    //[MenuItem("CONTEXT/CardPanel/ˢ��")]
    //    //private static void RefreshPanel(MenuCommand command)
    //    //{
    //    //    CardPanel cardPanel=command.context as CardPanel;
    //    //    cardPanel.RefreshPage();
    //    //}
    //    //[MenuItem("CONTEXT/CardPanel/���")]
    //    //private static void ClearPanel(MenuCommand command)
    //    //{
    //    //    CardPanel cardPanel = command.context as CardPanel;
    //    //    cardPanel.DestroyAllChildren();
    //    //}
    //    [ContextMenu("CardPanel")]
    //    private static void Card()
    //    {

    //    }
    //}
}
