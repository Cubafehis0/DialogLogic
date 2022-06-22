using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLibrary : Singleton<DynamicLibrary>
{
    [SerializeField]
    protected Dictionary<CardBase, CardObjectBase> cardDictionary = new Dictionary<CardBase, CardObjectBase>();
    [SerializeField]
    protected CardObjectBase sampleCard;


    public void DestroyCard(CardBase card)
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



    public CardObjectBase GetCardObject(CardBase card)
    {
        if (card == null) return null;
        if (cardDictionary.TryGetValue(card,out var go))
        {
            return go;
        }
        else return GetNewCardObject(card);
    }

    public CardObjectBase GetNewCardObject(CardBase card)
    {
        cardDictionary[card] = Instantiate(sampleCard);
        cardDictionary[card].SetCard(card);
        //card.player = CardGameManager.Instance.playerState;
        return cardDictionary[card];
    }
}
