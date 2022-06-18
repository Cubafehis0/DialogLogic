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
    private CardLibrary library = new CardLibrary();
    [SerializeField]
    private Dictionary<Card, CardObject> cardDictionary = new Dictionary<Card, CardObject>();
    [SerializeField]
    private CardObject sampleCard;

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

    public void Construct()
    {
        //((ICardLibrary)library).Construct();
    }

    public void DeclareCard(CardInfo cardInfo)
    {
        ((ICardLibrary)library).DeclareCard(cardInfo);
    }

    public void DeclareCard(List<CardInfo> cardInfos)
    {
        ((ICardLibrary)library).DeclareCard(cardInfos);
    }

    public Card GetCopyByName(string name)
    {
        Card card = ((ICardLibrary)library).GetCopyByName(name);
        cardDictionary[card] = null;
        return card;
    }

    public Card CopyCard(Card card)
    {
        Card newCard = ((ICardLibrary)library).CopyCard(card);
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

    public List<string> GetAllCards()
    {
        return ((ICardLibrary)library).GetAllCards();
    }

    public List<string> GetRandom(int cnt)
    {
        return ((ICardLibrary)library).GetRandom(cnt);
    }
}
