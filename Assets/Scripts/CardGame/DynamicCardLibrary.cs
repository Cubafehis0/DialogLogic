using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCardLibrary : Singleton<DynamicCardLibrary>
{
    private Dictionary<string, Card> cardsDictionary = new Dictionary<string, Card>();

    int cnt = 0;
    public Card AddNewCard(string name)
    {
        Card newCard = StaticCardLibrary.Instance.GetCopyByName(name);
        string id = $"card_{cnt++}";
        cardsDictionary.Add(id, newCard);
        newCard.id = id;
        DynamicLibrary.Instance.GetCardObject(newCard);
        return newCard;
    }

    public Card CopyCard(Card origin)
    {
        Card newCard = new Card();
        newCard.Construct(newCard);
        cardsDictionary.Add(origin.id, newCard);
        string id = $"card_{cnt++}";
        newCard.id = id;
        cardsDictionary.Add(id, newCard);
        return newCard;
    }

    public Card GetCard(string id)
    {
        return cardsDictionary[id];
    }
}
