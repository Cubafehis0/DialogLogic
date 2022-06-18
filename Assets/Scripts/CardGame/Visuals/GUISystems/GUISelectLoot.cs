using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUISelectLoot : ForegoundGUISystem
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private List<CardObject> cardObjects;

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is List<string> context)) return;
        if (context == null) return;
        title.text = "选择一张加入牌库";
        if (context.Count > cardObjects.Count)
        {
            Debug.LogWarning("战利品格数不足，可能有未显示");
        }
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].gameObject.SetActive(i < context.Count);
            if (i < context.Count)
            {
                cardObjects[i].Card = GameManager.Instance.CardLibrary.GetCopyByName(context[i]);
            }
        }
    }

    public void SelectCard(BaseEventData eventData)
    {
        CardObject c = ((PointerEventData)eventData).pointerClick.GetComponent<CardObject>();
        if (c == null) return;
        if (cardObjects.Contains(c))
        {
            GameManager.Instance.LocalPlayer.PlayerInfo.CardSet.Add(c.Card.info.Name);
            Close();
        }
    }
}
