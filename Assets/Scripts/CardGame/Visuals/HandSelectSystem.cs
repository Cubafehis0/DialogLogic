using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSelectSystem : MonoBehaviour
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

    private static HandSelectSystem instance = null;
    private Pile<Card> cardCandidate = new Pile<Card>();
    private Pile<Card> cardSelected = new Pile<Card>();
    private IEffect action = null;
    public static HandSelectSystem Instance { get => instance; }

    private void Awake()
    {
        instance = this;
        selectedCardPile.Pile = cardSelected;
        handPile.Pile = cardCandidate;
        gameObject.SetActive(false);
    }

    public bool Open(IReadOnlyList<Card> cards, int num, IEffect action)
    {
        if (gameObject.activeSelf) return false;
        gameObject.SetActive(true);
        foreach (Card card in cards)
        {
            cardCandidate.Add(card);
        }
        minOccurs = num;
        maxOccurs = num;
        this.action = action;
        UpdateVisuals();
        return true;
    }

    public void SelectCard(BaseEventData eventData)
    {
        Card card = ((PointerEventData)eventData).pointerClick.GetComponent<Card>();
        if (cardSelected.Count == maxOccurs) return;
        cardCandidate.Remove(card);
        cardSelected.Add(card);
        UpdateVisuals();
    }

    public void CancelCard(BaseEventData eventData)
    {
        Card card = ((PointerEventData)eventData).pointerClick.GetComponent<Card>();
        cardSelected.Remove(card);
        cardCandidate.Add(card);
        UpdateVisuals();
    }

    public void Confirm()
    {
        if (minOccurs <= cardSelected.Count && cardSelected.Count <= maxOccurs)
        {

            gameObject.SetActive(false);
            DragHandPileObject.instance.TakeoverAllCard();
            Context.PushPlayerContext(CardGameManager.Instance.player);
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
            CardGameManager.Instance.WaitGUI = false;
        }
    }

    public void UpdateVisuals()
    {
        if (UIText) UIText.text = string.Format("选择{0}张,已选择{1}张", minOccurs,cardSelected.Count);
    }

}
