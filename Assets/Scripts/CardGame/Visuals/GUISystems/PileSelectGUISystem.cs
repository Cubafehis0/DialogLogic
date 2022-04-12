﻿using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PileSelectGUIContext
{
    public List<Card> cards;
    public int num;
    public IEffect action;

    public PileSelectGUIContext(List<Card> cards, int num, IEffect action)
    {
        this.cards = cards;
        this.num = num;
        this.action = action;
    }
}

public class PileSelectGUISystem : ForegoundGUISystem
{
    [SerializeField]
    private RectTransform content;

    private int minOccurs = 0;
    private int maxOccurs = 0;
    private IEffect action = null;
    private List<CardObject> cardObjects = new List<CardObject>();
    private List<Card> cardSelected = new List<Card>();


    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is PileSelectGUIContext context)) return;
        cardObjects.Clear();
        cardSelected.Clear();
        minOccurs = context.num;
        maxOccurs = context.num;
        action = context.action;
        context.cards.ForEach(t =>
        {
            Card tmpCard = StaticCardLibrary.Instance.CopyCard(t);
            CardObject item = StaticCardLibrary.Instance.GetCardObject(tmpCard);
            item.gameObject.SetActive(true);
            item.transform.SetParent(content, true);
            cardObjects.Add(item);
        });
    }
    public void ClickCard(BaseEventData eventData)
    {
        CardObject c = ((PointerEventData)eventData).pointerClick.GetComponentInParent<CardObject>();
        if (c == null) return;
        Card card = c.Card;
        if (cardSelected.Contains(card))
        {
            cardSelected.Remove(card);
        }
        else
        {
            cardSelected.Add(card);
        }
    }

    public void Confirm()
    {
        if(minOccurs<= cardSelected.Count && cardSelected.Count <= maxOccurs)
        {
            foreach (Card card in cardSelected)
            {
                Context.PushCardContext(card);
                action?.Execute();
                Context.PopCardContext();
            }
            foreach(CardObject cardObject in cardObjects)
            {
                StaticCardLibrary.Instance.DestroyCard(cardObject.Card);
            }
            cardObjects.Clear();
            cardSelected.Clear();
            gameObject.SetActive(false);
            Close();
        }
    }

}