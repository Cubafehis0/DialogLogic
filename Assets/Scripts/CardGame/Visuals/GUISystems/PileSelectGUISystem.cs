using JasperMod.SemanticTree;
using ModdingAPI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PileSelectGUIContext
{
    public List<Card> cards;
    public int num;
    public Action action;

    public PileSelectGUIContext(List<Card> cards, int num, Action action)
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
    [SerializeField]
    private Text title;

    private int minOccurs = 0;
    private int maxOccurs = 0;
    private Action action = null;
    private List<CardObject> cardObjects = new List<CardObject>();
    private List<Card> cardSelected = new List<Card>();


    public override void Open(object msg)
    {
        if (!(msg is PileSelectGUIContext context)) return;
        if (context.num >= context.cards.Count)
        {
            foreach (Card card in context.cards)
            {
                Context.SetCardAlias("Item", card.id);
                action?.Invoke();
                Context.SetCardAlias("Item", "");
            }
            return;
        }
        if (context.num == 0)
        {
            return;
        }
        base.Open(msg);
        cardObjects.Clear();
        cardSelected.Clear();
        minOccurs = context.num;
        maxOccurs = context.num;
        action = context.action;
        context.cards.ForEach(t =>
        {
            Card tmpCard = GameManager.Instance.CardLibrary.CopyCard(t);
            CardObject item = GameManager.Instance.CardObjectLibrary.GetCardObject(tmpCard);
            item.gameObject.SetActive(true);
            item.transform.SetParent(content, true);
            cardObjects.Add(item);
        });
        UpdateVisuals();
    }

    public override void Close()
    {

        foreach (CardObject cardObject in cardObjects)
        {
            GameManager.Instance.CardObjectLibrary.DestroyCard(cardObject.Card);
        }
        cardObjects.Clear();
        cardSelected.Clear();
        gameObject.SetActive(false);
        base.Close();
    }

    public void UpdateVisuals()
    {
        title.text = string.Format("选择{0}张，已选择{1}张", minOccurs, cardSelected.Count);
    }
    public void ClickCard(BaseEventData eventData)
    {

        CardObject c = ((PointerEventData)eventData).pointerClick.GetComponentInParent<CardObject>();
        if (c == null) return;
        Card card = c.Card;
        if (cardSelected.Contains(card))
        {

            cardSelected.Remove(card);
            UpdateVisuals();
        }
        else
        {
            if (cardSelected.Count < maxOccurs)
            {
                cardSelected.Add(card);
                UpdateVisuals();
            }
        }
    }

    public void Confirm()
    {
        if (minOccurs <= cardSelected.Count && cardSelected.Count <= maxOccurs)
        {
            foreach (Card card in cardSelected)
            {
                Context.SetCardAlias("Item", card.id);
                action?.Invoke();
                Context.SetCardAlias("Item", "");
            }
            Close();
        }
    }

}