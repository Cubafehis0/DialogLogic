using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCardLibrary:MonoBehaviour
{
    private static StaticCardLibrary instance = null;
    public static StaticCardLibrary Instance
    {
        get => instance;
    }

    [SerializeField]
    protected List<CardInfo> cards;
    [SerializeField]
    protected CardInfoTable cardInfoTable;
    [SerializeField]
    public List<Card> cardObjects = new List<Card>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        //cards = cardInfoTable.cardInfos;
        //foreach (CardInfo card in cards)
        //{
        //    Card newCard = CardGameManager.Instance.EmptyCard;
        //    newCard.Construct(card);
        //    newCard.transform.SetParent(transform,false);
        //    newCard.gameObject.SetActive(false);
        //    cardObjects.Add(newCard);
        //}
    }

    public CardInfo? GetCardByID(int id)
    {
        if (id < 0 || id >= cards.Count) return null;
        return cards[id];
    }

    public CardInfo GetCardByName(string name)
    {
        return cards.Find(it => it.title.Equals(name));
    }

}
