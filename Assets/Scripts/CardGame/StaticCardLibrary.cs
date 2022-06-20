using System.Collections.Generic;
using UnityEngine;
using ModdingAPI;

public interface ICardObjectLibrary
{
    CardObject GetCardObject(Card card);
    void DestroyCard(Card card);
}

public class StaticCardLibrary : MonoBehaviour, ICardObjectLibrary, ICardLibrary
{
    [SerializeField]
    private Dictionary<Card, CardObject> cardDictionary = new Dictionary<Card, CardObject>();
    [SerializeField]
    private CardObject sampleCard;

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
        //Debug.Log($"×¢²á¿¨ÅÆ{cardInfo.Name}");
        if (!cardDic.ContainsKey(cardInfo.Name))
        {
            cardDic[cardInfo.Name] = new Card(cardInfo);
        }

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

    private static StaticCardLibrary instance;
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

    public Card GetCopyByName(string name)
    {
        Card card = CopyCard(cardDic[name]);
        cardDictionary[card] = null;
        return card;
    }

    public Card CopyCard(Card card)
    {
        Card newCard =  new Card(card); ;
        cardDictionary[newCard] = null;
        return newCard;
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



    public void DestroyCard(Card card)
    {
        if (cardDictionary[card] != null)
        {
            Destroy(cardDictionary[card].gameObject);
            cardDictionary.Remove(card);
        }
        else
        {
            Debug.LogError("Î´ÖªµÄ¿¨ÅÆ");
        }
    }
}
