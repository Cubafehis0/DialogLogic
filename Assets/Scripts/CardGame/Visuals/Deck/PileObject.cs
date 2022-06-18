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

    private void OnAddAnim(Card card)
    {

        CardObject cardObject = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        if (cardObject)
        {
            cardObject.transform.SetParent(transform, true);
            cardObject.gameObject.SetActive(true);
        }
    }
}