using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;
public class CardLibrary : ICardLibrary
{
    [SerializeField]
    private Dictionary<string, Card> cardDic = new Dictionary<string, Card>();

    public void DeclareCard(List<CardInfo> cardInfos)
    {
        foreach (CardInfo cardInfo in cardInfos)
        {
            DeclareCard(cardInfo);
        }
    }

    public void DeclareCard(CardInfo cardInfo)
    {
        if (!cardDic.ContainsKey(cardInfo.Name))
        {
            cardDic[cardInfo.Name] = new Card(cardInfo);
        }

    }

    public Card GetCopyByName(string name)
    {
        if (cardDic.ContainsKey(name))
        {
            return CopyCard(cardDic[name]);
        }
        return null;
    }

    public Card CopyCard(Card card)
    {
        Card newCard = new Card(card);
        return newCard;
    }

    public List<string> GetAllCards()
    {
        List<string> res = new List<string>();
        foreach (string name in cardDic.Keys)
        {
            res.Add(name);
        }
        return res;
    }

    public List<string> GetRandom(int cnt = 1)
    {
        List<string> allCards = GetAllCards();
        MyMath.Shuffle(allCards);
        return allCards.GetRange(0, cnt);
    }
}
