using System.Collections;
using UnityEngine;
public class DragHandPileObject : PilePacked
{
    public static DragHandPileObject instance;

    [SerializeField]
    private Transform layout;
    [SerializeField]
    private bool hide;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("不允许多个HandsArrangement单例");
        }
    }

    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
        OnRemove.AddListener(OnRemoveAnim);
    }

    private void OnDisable()
    {
        OnAdd.RemoveListener(OnAddAnim);
        OnRemove.RemoveListener(OnRemoveAnim);
    }

    private void OnAddAnim(Card card)
    {
        Debug.Log("456");
        CardObject cardObject = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        AnimationManager.Instance.AddAnimation(Animating(cardObject.gameObject));
    }

    private IEnumerator Animating(GameObject cardObject)
    {
        cardObject.SetActive(true);
        cardObject.transform.SetParent(layout, true);
        cardObject.transform.rotation = Quaternion.identity;
        cardObject.transform.localScale = Vector3.one;
        cardObject.GetComponent<CardObject>().Draggable = true;
        yield return null;
    }

    public void SetEnableDragging(bool value)
    {
        foreach (var card in this)
        {
            CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
            o.Draggable = value;
        }
    }

    protected virtual void OnRemoveAnim(Card card)
    {
        CardObject cardObject = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        if (cardObject)
        {
            cardObject.Draggable = false;
        }

    }
    public void TakeoverAllCard()
    {
        Debug.Log("takeover");
        foreach (Card card in this)
        {
            CardObject o = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
            o.transform.SetParent(layout, true);
        }
    }

    public void Hide()
    {
        hide = true;
        UpdateVisuals();
    }

    public void ExposeHand()
    {
        hide = false;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        RectTransform rect = layout.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, hide ? 0f : 1f);
    }

    public void Switch()
    {
        hide = !hide;
        UpdateVisuals();
    }


}
