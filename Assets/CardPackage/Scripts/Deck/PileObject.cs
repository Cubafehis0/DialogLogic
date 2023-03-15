using UnityEngine;
public class PileObject : PilePacked
{

    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
    }

    private void OnDisable()
    {
        OnAdd.RemoveListener(OnAddAnim);
    }

    private void OnAddAnim(CardBase card)
    {

        GameObject cardObject = DynamicLibrary.Instance.GetCardObject((Card)card).gameObject;
        if (cardObject)
        {
            cardObject.transform.SetParent(transform, true);
            cardObject.gameObject.SetActive(true);
        }
    }
}