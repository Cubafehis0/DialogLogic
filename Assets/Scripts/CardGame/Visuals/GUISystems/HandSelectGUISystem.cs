using JasperMod.SemanticTree;
using ModdingAPI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSelectGUIContext
{
    public IReadOnlyList<Card> cards;
    public Func<bool> condition;
    public int num;
    public Action action;

    public HandSelectGUIContext(IReadOnlyList<Card> cards, Func<bool> condition, int num, Action action)
    {
        this.cards = cards;
        this.num = num;
        this.condition = condition;
        this.action = action;
    }
}


public class HandSelectGUISystem : ForegoundGUISystem
{
    [SerializeField]
    private Text UIText;
    [SerializeField]
    private PilePacked handPile;
    [SerializeField]
    private PilePacked selectedCardPile;
    [SerializeField]
    private int minOccurs = 0;
    [SerializeField]
    private int maxOccurs = 0;


    private Action action = null;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is HandSelectGUIContext context)) return;
        gameObject.SetActive(true);
        foreach (Card card in context.cards)
        {
            handPile.Add(card);
        }
        minOccurs = context.num;
        maxOccurs = context.num;
        action = context.action;
        CardGameManager.Instance.handPileObject.SetEnableDragging(false);
        UpdateVisuals();
    }

    public override void Close()
    {
        base.Close();
        CardGameManager.Instance.handPileObject.SetEnableDragging(true);
        gameObject.SetActive(false);
    }

    public void SelectCard(BaseEventData eventData)
    {
        CardObject c = ((PointerEventData)eventData).pointerClick.GetComponent<CardObject>();
        if (c == null) return;
        if (selectedCardPile.Count == maxOccurs) return;
        handPile.Remove(c.GetCard<CardBase>());
        selectedCardPile.Add((Card)c.GetCard<CardBase>());
        UpdateVisuals();
    }

    public void CancelCard(BaseEventData eventData)
    {
        CardObject cardObject = ((PointerEventData)eventData).pointerClick.GetComponent<CardObject>();
        if (cardObject == null) return;
        selectedCardPile.Remove(cardObject.GetCard<CardBase>());
        handPile.Add(cardObject.GetCard<CardBase>());
        UpdateVisuals();
    }

    public void Confirm()
    {
        if (minOccurs <= selectedCardPile.Count && selectedCardPile.Count <= maxOccurs)
        {
            DragHandPileObject.instance.TakeoverAllCard();
            //有缺陷
            //Context.PushPileContext(cardSelected);
            foreach (Card card in selectedCardPile)
            {
                Context.SetCardAlias("Item", card.id);
                action.Invoke();
                Context.SetCardAlias("Item", card.id);
            }
            //Context.PopPileContext();
            handPile.Clear();
            selectedCardPile.Clear();
            Close();
        }
    }

    public void UpdateVisuals()
    {
        if (UIText) UIText.text = string.Format("选择{0}张,已选择{1}张", minOccurs, selectedCardPile.Count);
    }

}
