using SemanticTree;
using SemanticTree.PileSemantics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelectSystem : MonoBehaviour
{
    [SerializeField]
    private SelectablePileObject handPile;
    [SerializeField]
    private SelectablePileObject selectedCardPile;

    private int minOccurs = 0;
    private int maxOccurs = 0;
    private IEffectNode action = null;
    private static HandSelectSystem instance = null;
    private Pile<Card> cardCandidate = new Pile<Card>();
    private Pile<Card> cardSelected = new Pile<Card>();
    private List<CardObject> cardObjects = new List<CardObject>();
    public static HandSelectSystem Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        handPile.Pile = cardCandidate;
        selectedCardPile.Pile = cardSelected;
        handPile.OnSelectCard.AddListener(SelectCard);
        selectedCardPile.OnSelectCard.AddListener(CancelCard);
    }

    public void Open(List<Card> cards, int num, IEffectNode action)
    {
        cardObjects.Clear();
        cards.ForEach(item => cardCandidate.Add(item));
        minOccurs = num;
        maxOccurs = num;
        this.action = action;
    }

    private void SelectCard(Card card)
    {
        cardCandidate.Remove(card);
        cardSelected.Add(card);
    }

    private void CancelCard(Card card)
    {
        cardSelected.Remove(card);
        cardCandidate.Add(card);
    }

    public void Confirm()
    {
        if (minOccurs <= cardSelected.Count && cardSelected.Count <= maxOccurs)
        {
            PileNode.PushCardContext(cardSelected);
            action.Execute();
            PileNode.PopCardContext();
        }
    }

}
