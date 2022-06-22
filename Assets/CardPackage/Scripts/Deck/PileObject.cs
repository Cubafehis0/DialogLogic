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

        CardObjectBase cardObject = Singleton<DynamicLibrary>.Instance.GetCardObject(card);
        if (cardObject)
        {
            cardObject.transform.SetParent(transform, true);
            cardObject.gameObject.SetActive(true);
        }
    }
}