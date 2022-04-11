using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUIDiscoverInfo
{
    public List<Card> cards;
}

public class GUIDiscoverCard : ForegoundGUISystem
{
    [SerializeField]
    private Text title;
    [SerializeField]
    private List<Card> cardObjects;

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is GUIDiscoverInfo context)) return;
        if (context.cards == null) return;
        title.text = "选择一张加入牌库";
        for(int i = 0; i < cardObjects.Count; i++)
        {
            cardObjects[i].gameObject.SetActive(i < context.cards.Count);
            if(i < context.cards.Count)
            {
                cardObjects[i].Construct(context.cards[i]);
            }
        }
    }

    public void Add2CardSet()
    {
        GameObject o = EventSystem.current.currentSelectedGameObject;
        if (o == null) return;
        Card c = o.GetComponentInParent<Card>();
        if (c == null) return;
        if (cardObjects.Contains(c))
        {
            GameManager.Instance.localPlayer.PlayerInfo.CardSet.Add(c.info.Name);
        }
    }
}
