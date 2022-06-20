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

    private void OnAddAnim(Card newCard)
    {

        CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(newCard);
        if (o == null)
        {
            throw new System.NotImplementedException();
        }
        o.transform.SetParent(transform, true);
        o.transform.localPosition = Vector3.zero;
        o.gameObject.SetActive(false);
    }
}
