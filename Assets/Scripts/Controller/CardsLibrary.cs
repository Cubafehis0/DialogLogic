using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsLibrary : MonoBehaviour
{
    public static CardsLibrary instance = null;
    public CardObject[] prefabs = null;


    public CardObject GetCardPrefab(int id)
    {
        if (0 <= id && id < prefabs.Length)
            return prefabs[id];
        return null;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
}
