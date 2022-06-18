using UnityEditor;
using UnityEngine;
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
            if (GUILayout.Button("刷新界面"))
            {
                //myScript.RefreshPage();
            }
            if (GUILayout.Button("清除界面"))
            {
                //myScript.DestroyAllChildren();
            }
        }
    }
    //public class CardPsanelEditor
    //{
    //    //[MenuItem("CONTEXT/CardPanel/刷新")]
    //    //private static void RefreshPanel(MenuCommand command)
    //    //{
    //    //    CardPanel cardPanel=command.context as CardPanel;
    //    //    cardPanel.RefreshPage();
    //    //}
    //    //[MenuItem("CONTEXT/CardPanel/清除")]
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
