using System.Collections.Generic;
using UnityEngine;
using SemanticTree;

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
        if (cardDic.ContainsKey(cardInfo.Name)) throw new SemanticException("不能重复定义卡牌" + cardInfo.Name);
        cardDic[cardInfo.Name] = new Card(cardInfo);
    }

    public void Construct()
    {
        foreach (Card card in cardDic.Values)
        {
            card.Construct();
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
        //cardDictionary[newCard] = null;
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
}
