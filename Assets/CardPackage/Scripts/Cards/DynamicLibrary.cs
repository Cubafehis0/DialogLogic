using System;
using System.Collections.Generic;
using UnityEngine;


public class DynamicLibrary : DynamicLibraryBase<CardBase> 
{
    public override GameObject GetNewCardObject(CardBase card)
    {
        var res= base.GetNewCardObject(card);
        res.GetComponent<CardObject>().SetCard((Card)card);
        return res;
    }
}
public class DynamicLibraryBase<T> : Singleton<DynamicLibraryBase<T>>
{
    [SerializeField]
    protected Dictionary<T, GameObject> cardDictionary = new Dictionary<T, GameObject>();
    [SerializeField]
    protected GameObject sampleCard;


    public void DestroyCard(T card)
    {
        if (cardDictionary[card] != null)
        {
            Destroy(cardDictionary[card]);
            cardDictionary.Remove(card);
        }
        else
        {
            Debug.LogError("Î´ÖªµÄ¿¨ÅÆ");
        }
    }


    public virtual GameObject GetNewCardObject(T card)
    {
        cardDictionary[card] = Instantiate(sampleCard);
        
        //card.player = CardGameManager.Instance.playerState;
        return cardDictionary[card];
    }
    public GameObject GetCardObject(T card)
    {
        if (card == null) return null;
        if (cardDictionary.TryGetValue(card,out var go))
        {
            return go;
        }
        else return GetNewCardObject(card);
    }


}
