using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibrary : MonoBehaviour
{
    [SerializeField]
    protected List<Card> cards = new List<Card>();

    //[SerializeField]
    //private CardTable cardTable;
    //private void ParseCardData()
    //{
    //    foreach (CardEntity entity in cardTable.Sheet1)
    //    {
    //        cards.Add(entity.id,
    //            new Card(entity.id, entity.hold_effect, entity.hold_effect_scale, entity.condition, entity.condition_scale, entity.effect, entity.effect_scale, entity.post_effect, entity.post_effect_scale));
    //    }
    //}

    public Card GetCardByID(uint id)
    {
        Debug.Log("GetCardByID:" + id);
        if (0 <= id && id < cards.Count)
        {
            return cards[(int)id];
        }
        else
        {
            return null;
        }
    }

    public Card GetCardByID(int id)
    {
        Debug.Log("GetCardByID:" + id);
        if (0 <= id && id < cards.Count)
        {
            return cards[id];
        }
        else
        {
            return null;
        }
    }

    public Card GetCardCopyByID(int id)
    {
        if (0 <= id && id < cards.Count)
        {
            return Instantiate(cards[id]);
        }
        else
        {
            return null;
        }
    }

}
