using UnityEngine;

public class ExhaustPileObject : PilePacked
{
    [SerializeField]
    private CardController player;

    private void OnEnable()
    {
        OnAdd.AddListener(PlayExhaustAnim);
    }

    private void PlayExhaustAnim(Card newCard)
    {
        CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
