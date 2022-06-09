using SemanticTree;
using SemanticTree.Condition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSelectGUIContext
{
    public IReadOnlyList<Card> cards;
    public ICondition condition;
    public int num;
    public IEffect action;

    public HandSelectGUIContext(IReadOnlyList<Card> cards,ICondition condition, int num, IEffect action)
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

 
    private IEffect action = null;

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
        handPile.Remove(c.Card);
        selectedCardPile.Add(c.Card);
        UpdateVisuals();
    }

    public void CancelCard(BaseEventData eventData)
    {
        CardObject cardObject = ((PointerEventData)eventData).pointerClick.GetComponent<CardObject>();
        if (cardObject == null) return;
        selectedCardPile.Remove(cardObject.Card);
        handPile.Add(cardObject.Card);
        UpdateVisuals();
    }

    public void Confirm()
    {
        if (minOccurs <= selectedCardPile.Count && selectedCardPile.Count <= maxOccurs)
        {
            DragHandPileObject.instance.TakeoverAllCard();
            //��ȱ��
            //Context.PushPileContext(cardSelected);
            foreach(Card card in selectedCardPile)
            {
                Context.PushCardContext(card);
                action?.Execute();
                Context.PopCardContext();
            }
            //Context.PopPileContext();
            handPile.Clear();
            selectedCardPile.Clear();
            Close();
        }
    }

    public void UpdateVisuals()
    {
        if (UIText) UIText.text = string.Format("ѡ��{0}��,��ѡ��{1}��", minOccurs, selectedCardPile.Count);
    }

}
