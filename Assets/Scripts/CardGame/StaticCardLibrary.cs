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

    public void Construct(List<Common> commons)
    {
        foreach (Common common in commons)
        {
            DeclareCard(common.CardInfos);
        }
        foreach (Common common in commons)
        {
            DefineCard(common.CardInfos);
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
        DeclareCard(cardInfo.Name);
    }

    public void DeclareCard(string name)
    {
        if (cardDictionary.ContainsKey(name)) throw new SemanticException("�����ظ����忨��" + name);
        //else Debug.Log("���ؿ���:" + name);
        Card newCard = Instantiate(sampleCard);
        newCard.transform.SetParent(transform, false);
        newCard.gameObject.SetActive(false);
        newCard.gameObject.name = name;
        cardDictionary.Add(name, newCard);
    }

    public void DefineCard(List<CardInfo> cardInfos)
    {
        foreach(CardInfo cardInfo in cardInfos)
        {
            DefineCard(cardInfo);
        }
    }
    public void DefineCard(CardInfo info)
    {
        string name = info.Name;
        if (!cardDictionary.ContainsKey(name)) DeclareCard(name);
        cardDictionary[name].Construct(info);
        info.Construct();
    }

    public Card GetByName(string name)
    {
        if (!cardDictionary.ContainsKey(name))
        {
            Debug.LogError(string.Format("��������Ϊ{0}�Ŀ���", name));
            return null;
        }
        return cardDictionary[name];
    }

    public CardObject GetCardObject(Card card)
    {
        GameObject gameObject = Instantiate(card.gameObject);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.rotation = Quaternion.identity;
        return gameObject.GetComponent<CardObject>();
    }

}
