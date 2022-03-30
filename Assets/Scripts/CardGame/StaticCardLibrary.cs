using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;
using SemanticTree;

public class StaticCardLibrary : MonoBehaviour
{
    

    [SerializeField]
    private Dictionary<string, Card> cardDictionary = new Dictionary<string, Card>();
    [SerializeField]
    private Card sampleCard;

    private static StaticCardLibrary instance = null;
    public static StaticCardLibrary Instance
    {
        get => instance;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void DeclareCard(string name)
    {
        if (cardDictionary.ContainsKey(name)) throw new SemanticException("不能重复定义卡牌");
        Card newCard = Instantiate(sampleCard);
        newCard.transform.SetParent(transform, false);
        newCard.gameObject.SetActive(false);
        newCard.gameObject.name = name;
        cardDictionary.Add(name, newCard);
    }

    public Card GetByName(string name)
    {
        return cardDictionary[name];
    }

    public CardObject GetCardObject(Card card)
    {
        GameObject gameObject = Instantiate(card.gameObject);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.rotation = Quaternion.identity;
        return gameObject.GetComponent<CardObject>();
    }

    public void DefineCard(CardInfo info)
    {
        string name = info.Name;
        if (!cardDictionary.ContainsKey(name)) DeclareCard(name);
        cardDictionary[name].Construct(info);
        info.Construct();
    }
}
