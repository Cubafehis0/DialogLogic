using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIDiscoverInfo
{
    public List<string> cards;
}

public class GUIDiscoverCard : ForegoundGUISystem
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private List<CardObject> cardObjects;

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is GUIDiscoverInfo context)) return;
        if (context.cards == null) return;
        title.text = "选择一张加入牌库";
        for (int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].gameObject.SetActive(i < context.cards.Count);
            if (i < context.cards.Count)
            {
                cardObjects[i].SetCard(StaticCardLibrary.Instance.GetCopyByName(context.cards[i]));
            }
        }
    }

    public void Add2CardSet()
    {
        GameObject o = EventSystem.current.currentSelectedGameObject;
        if (o == null) return;
        CardObject c = o.GetComponentInParent<CardObject>();
        if (c == null) return;
        if (cardObjects.Contains(c))
        {
            GameManager.Instance.LocalPlayer.PlayerInfo.CardSet.Add(c.GetCard<Card>().info.Name);
        }
    }
}
