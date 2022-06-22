using UnityEngine;

public class DrawPileObject : PilePacked
{
    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
    }
    private void OnDisable()
    {
        OnAdd.RemoveListener(OnAddAnim);
    }

    private void OnAddAnim(CardBase newCard)
    {

        CardObjectBase o = Singleton<DynamicLibrary>.Instance.GetCardObject(newCard);
        if (o == null)
        {
            throw new System.NotImplementedException();
        }
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
