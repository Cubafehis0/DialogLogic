using SemanticTree;
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
    [SerializeField]
    private int minOccurs = 0;
    [SerializeField]
    private int maxOccurs = 0;

    private static HandSelectSystem instance = null;
    private Pile<Card> cardCandidate = new Pile<Card>();
    private Pile<Card> cardSelected = new Pile<Card>();
    private IEffectNode action = null;
    public static HandSelectSystem Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        selectedCardPile.Pile = cardSelected;
        handPile.Pile = cardCandidate;
        gameObject.SetActive(false);
    }

    public bool Open(List<Card> cards, int num, IEffectNode action)
    {
        if (gameObject.activeSelf) return false;
        gameObject.SetActive(true);
        cards.ForEach(item => { 
            cardCandidate.Add(item);
        });
        minOccurs = num;
        maxOccurs = num;
        this.action = action;
        return true;
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

            gameObject.SetActive(false);
            DragHandPileObject.instance.TakeoverAllCard();
            Context.PushPlayerContext(CardPlayerState.Instance);
            //ÓÐÈ±ÏÝ
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
        }
    }

}
