using SemanticTree;
using SemanticTree.PileSemantics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandSelectSystem : MonoBehaviour
{
    [SerializeField]
    private PileObject handPile;
    [SerializeField]
    private PileObject selectedCardPile;

    private static HandSelectSystem instance = null;
    private int minOccurs = 0;
    private int maxOccurs = 0;
    private IEffectNode action = null;
    public static Pile<Card> cardCandidate = new Pile<Card>();
    public static Pile<Card> cardSelected = new Pile<Card>();
    private List<CardObject> cardObjects = new List<CardObject>();
    public static HandSelectSystem Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        selectedCardPile.Pile = cardSelected;
        handPile.Pile = cardCandidate;
        gameObject.SetActive(false);
    }

    public void Open(List<Card> cards, int num, IEffectNode action)
    {
        gameObject.SetActive(true);
        cardObjects.Clear();
        cards.ForEach(item => { 
            cardCandidate.Add(item);
        });
        minOccurs = num;
        maxOccurs = num;
        this.action = action;
    }

    public void SelectCard(BaseEventData eventData)
    {
        Card card = ((PointerEventData)eventData).pointerClick.GetComponent<Card>();
        if (cardSelected.Count == maxOccurs) return;
        cardCandidate.Remove(card);
        cardSelected.Add(card);
    }

    public void CancelCard(BaseEventData eventData)
    {
        Card card = ((PointerEventData)eventData).pointerClick.GetComponent<Card>();
        cardSelected.Remove(card);
        cardCandidate.Add(card);
    }

    public void Confirm()
    {
        if (minOccurs <= cardSelected.Count && cardSelected.Count <= maxOccurs)
        {
            cardCandidate.Clear();
            cardSelected.Clear();
            gameObject.SetActive(false);
            DragHandPileObject.instance.TakeoverAllCard();
            PileNode.PushCardContext(cardSelected);
            action?.Execute();
            PileNode.PopCardContext();
        }
    }

}
