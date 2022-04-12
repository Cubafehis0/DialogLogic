using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;
using SemanticTree;

public class StaticCardLibrary : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, Card> cardDic = new Dictionary<string, Card>();
    [SerializeField]
    private Dictionary<Card, CardObject> cardDictionary = new Dictionary<Card, CardObject>();
    [SerializeField]
    private CardObject sampleCard;

    private static StaticCardLibrary instance = null;
    public static StaticCardLibrary Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DeclareCard(List<CardInfo> cardInfos)
    {
        foreach (CardInfo cardInfo in cardInfos)
        {
            DeclareCard(cardInfo);
        }
    }

    public void DeclareCard(CardInfo cardInfo)
    {
        if (cardDic.ContainsKey(cardInfo.Name)) throw new SemanticException("�����ظ����忨��" + name);
        cardDic[cardInfo.Name] = new Card(cardInfo);
    }

    public void Construct()
    {
        foreach (Card card in cardDic.Values)
        {
            card.Construct();
        }
    }

    public Card GetByName(string name)
    {
        if (cardDic.ContainsKey(name))
        {
            return new Card(cardDic[name]);
        }
        return null;
    }

    public CardObject GetCardObject(Card card)
    {
        if (card == null) return null;
        if (cardDictionary[card] != null)
        {
            return cardDictionary[card];
        }
        else return GetNewCardObject(card);
    }

    public CardObject GetNewCardObject(Card card)
    {
        cardDictionary[card] = Instantiate(sampleCard);
        cardDictionary[card].Card = card;
        card.player = CardGameManager.Instance.playerState;
        return cardDictionary[card];
    }

    public Card CopyCard(Card card)
    {
        Card newCard= new Card(card);
        cardDictionary[newCard] = null;
        return newCard;
    }

    public void DestroyCard(Card card)
    {
        if(cardDictionary[card] != null)
        {
            Destroy(cardDictionary[card]);
            cardDictionary.Remove(card);
        }
        else
        {
            Debug.LogError("δ֪�Ŀ���");
        }
    }

}
