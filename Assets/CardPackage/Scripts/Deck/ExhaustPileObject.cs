using UnityEngine;

public class ExhaustPileObject : PilePacked
{
    [SerializeField]
    private CardControllerBase player;

    private void OnEnable()
    {
        OnAdd.AddListener(PlayExhaustAnim);
    }

    private void PlayExhaustAnim(CardBase newCard)
    {
        CardObjectBase o = Singleton<DynamicLibrary>.Instance.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
