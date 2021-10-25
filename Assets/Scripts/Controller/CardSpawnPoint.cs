using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardSpawnPoint : MonoBehaviour
{

    [SerializeField]
    private float defaultBreakTime = 1f;
    public CardObject CreateCardObject(int id, UnityAction<CardObject> callback = null)
    {
        return CreateCardObject(CardsLibrary.instance.GetCardPrefab(id), callback);
    }

    public CardObject CreateCardObject(CardObject prefab, UnityAction<CardObject> callback = null)
    {
        CardObject newCard = Instantiate(prefab, transform.position, Quaternion.identity);
        if (callback != null) callback.Invoke(newCard);
        return newCard;
    }


    public IEnumerator CreateBunch(CardObject[] prefabs, UnityAction<CardObject> callback = null)
    {
        yield return StartCoroutine(CreateBunch(prefabs, defaultBreakTime, callback));
    }

    public IEnumerator CreateBunch(int[] ids, UnityAction<CardObject> callback = null)
    {
        yield return StartCoroutine(CreateBunch(ids, defaultBreakTime, callback));
    }
    public IEnumerator CreateBunch(CardObject[] prefabs, float minBreakTime, UnityAction<CardObject> callback = null)
    {
        foreach (CardObject prefab in prefabs)
        {
            CardObject newCard = CreateCardObject(prefab, callback);
            yield return new WaitForSeconds(minBreakTime);
        }
    }

    public IEnumerator CreateBunch(int[] ids, float minBreakTime, UnityAction<CardObject> callback = null)
    {
        foreach (int id in ids)
        {
            CardObject newCard = CreateCardObject(id, callback);
            yield return new WaitForSeconds(minBreakTime);
        }
    }
}
