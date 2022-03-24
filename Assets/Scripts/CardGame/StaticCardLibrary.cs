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

    public void DeclareCard(XmlNode xmlNode)
    {
        if (!xmlNode.Name.Equals("define_card")) return;
        string name = xmlNode.Attributes["name"].InnerText;
        DeclareCard(name);
    }

    public void DeclareCard(string name)
    {
        if (cardDictionary.ContainsKey(name)) throw new SemanticException("�����ظ����忨��");
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

    public void DefineCard(XmlNode xmlNode)
    {
        if (!xmlNode.Name.Equals("define_card")) return;
        string name = xmlNode.Attributes["name"].InnerText;
        if (!cardDictionary.ContainsKey(name)) DeclareCard(xmlNode);
        cardDictionary[name].Construct(xmlNode);
    }
}
