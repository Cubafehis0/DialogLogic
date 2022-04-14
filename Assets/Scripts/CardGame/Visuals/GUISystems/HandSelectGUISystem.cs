using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSelectGUIContext
{
    public IReadOnlyList<Card> cards;
    public int num;
    public IEffect action;

    public HandSelectGUIContext(IReadOnlyList<Card> cards, int num, IEffect action)
    {
        this.cards = cards;
        this.num = num;
        this.action = action;
    }
}


public class HandSelectGUISystem : ForegoundGUISystem
{
    [SerializeField]
    private Text UIText;
    [SerializeField]
    private PileObject handPile;
    [SerializeField]
    private PileObject selectedCardPile;
    [SerializeField]
    private int minOccurs = 0;
    [SerializeField]
    private int maxOccurs = 0;

    private Pile<Card> cardCandidate = new Pile<Card>();
    private Pile<Card> cardSelected = new Pile<Card>();
    private IEffect action = null;

    private void Awake()
    {
        selectedCardPile.Pile = cardSelected;
        handPile.Pile = cardCandidate;
        gameObject.SetActive(false);
    }

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is HandSelectGUIContext context)) return;
        gameObject.SetActive(true);
        foreach (Card card in context.cards)
        {
            cardCandidate.Add(card);
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
        if (cardSelected.Count == maxOccurs) return;
        cardCandidate.Remove(c.Card);
        cardSelected.Add(c.Card);
        UpdateVisuals();
    }

    public void CancelCard(BaseEventData eventData)
    {
        CardObject cardObject = ((PointerEventData)eventData).pointerClick.GetComponent<CardObject>();
        if (cardObject == null) return;
        cardSelected.Remove(cardObject.Card);
        cardCandidate.Add(cardObject.Card);
        UpdateVisuals();
    }

    public void Confirm()
    {
        if (minOccurs <= cardSelected.Count && cardSelected.Count <= maxOccurs)
        {

            gameObject.SetActive(false);
            DragHandPileObject.instance.TakeoverAllCard();
            Context.PushPlayerContext(CardGameManager.Instance.playerState);
            //有缺陷
            Context.PushPileContext(cardSelected);
            foreach(Card card in cardSelected)
            {
                Context.PushCardContext(card);
                action?.Execute();
                Context.PopCardContext();
            }
            Context.PopPileContext();
            Context.PopPlayerContext();
            cardCandidate.Clear();
            cardSelected.Clear();
            Close();
        }
    }

    public void UpdateVisuals()
    {
        if (UIText) UIText.text = string.Format("选择{0}张,已选择{1}张", minOccurs,cardSelected.Count);
    }

}
