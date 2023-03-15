using UnityEngine;

public class ExhaustPileObject : PilePacked
{

    private void OnEnable()
    {
        OnAdd.AddListener(PlayExhaustAnim);
    }

    private void PlayExhaustAnim(CardBase newCard)
    {
        GameObject o = DynamicLibrary.Instance.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.SetActive(false);
    }
}
