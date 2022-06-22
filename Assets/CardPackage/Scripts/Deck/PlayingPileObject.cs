using UnityEngine;

public class PlayingPileObject : PilePacked
{
    [SerializeField]
    private CardController player;

    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
    }

    private void OnAddAnim(CardBase newCard)
    {
        CardObjectBase o =  Singleton<DynamicLibrary>.Instance.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
    }
}
