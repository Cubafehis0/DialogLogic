using UnityEngine;

public class PlayingPileObject : PilePacked
{

    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
    }

    private void OnAddAnim(CardBase newCard)
    {
        GameObject o =  DynamicLibrary.Instance.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
    }
}
