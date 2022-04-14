using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    [SerializeField]
    private Text title;

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
        if (cardSelected.Count >= maxOccurs) return;
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
            cardSelected.Add(card);
            UpdateVisuals();
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
            Close();
        }
    }

}