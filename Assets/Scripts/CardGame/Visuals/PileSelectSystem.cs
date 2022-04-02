using SemanticTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PileSelectSystem : MonoBehaviour
{
    [SerializeField]
    private RectTransform content;

    private static PileSelectSystem instance=null;
    private int minOccurs = 0;
    private int maxOccurs = 0;
    private IEffect action = null;
    private List<CardObject> cardObjects = new List<CardObject>();
    private List<Card> cardSelected = new List<Card>();
    public static PileSelectSystem Instance { get => instance;}


    private void Awake()
    {
        gameObject.SetActive(false);
        instance = this;
    }
    public void Open(List<Card> cards, int num, IEffect action)
    {
        if (gameObject.activeSelf) return;
        gameObject.SetActive(true);
        cardObjects.Clear();
        cardSelected.Clear();
        this.minOccurs = num;
        this.maxOccurs = num;
        this.action = action;
        cards.ForEach(t =>
        {
            CardObject item = StaticCardLibrary.Instance.GetCardObject(t);
            item.gameObject.SetActive(true);
            item.transform.SetParent(content, true);
            cardObjects.Add(item);
        });
    }

    public void ClickCard(BaseEventData eventData)
    {
        var card = ((PointerEventData)eventData).pointerClick.GetComponent<Card>();
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
            CardGameManager.Instance.WaitGUI = false;
        }
    }

}