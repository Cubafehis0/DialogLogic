using SemanticTree;
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


    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is PileSelectGUIContext context)) return;
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        cardObjects.Clear();
        cardSelected.Clear();
        this.minOccurs = context.num;
        this.maxOccurs = context.num;
        this.action = context.action;
        context.cards.ForEach(t =>
        {
            CardObject item = StaticCardLibrary.Instance.GetCardObject(t);
            item.gameObject.SetActive(true);
            item.transform.SetParent(content, true);
            cardObjects.Add(item);
        });
    }

    public override void Close()
    {
        base.Close();
        gameObject.SetActive(false);
    }
    public void ClickCard(BaseEventData eventData)
    {
        CardObject c = ((PointerEventData)eventData).pointerClick.GetComponent<CardObject>();
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
            cardObjects.ForEach(item => CardGameManager.Instance.ReturnCardObject(item));
            cardObjects.Clear();
            cardSelected.Clear();
            gameObject.SetActive(false);
            Close();
        }
    }

}