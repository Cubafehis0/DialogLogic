using System.Collections;
using UnityEngine;

public class HandObjectBase : PilePacked
{
    public static HandObjectBase instance;
    [SerializeField]
    private Transform layout;
    [SerializeField]
    private bool hide;

    public void ExposeHand()
    {
        hide = false;
        UpdateVisuals();
    }

    public void Hide()
    {
        hide = true;
        UpdateVisuals();
    }

    public void SetEnableDragging(bool value)
    {
        foreach (var card in this)
        {
            CardObjectBase o = Singleton<DynamicLibrary>.Instance.GetCardObject(card);
            o.SetDraggable(value);
        }
    }

    public void Switch()
    {
        hide = !hide;
        UpdateVisuals();
    }
    public void TakeoverAllCard()
    {
        Debug.Log("takeover");
        foreach (CardBase card in this)
        {
            CardObjectBase o = Singleton<DynamicLibrary>.Instance.GetCardObject(card);
            o.transform.SetParent(layout, true);
        }
    }

    public void UpdateVisuals()
    {
        RectTransform rect = layout.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, hide ? 0f : 1f);
    }

    protected virtual void OnRemoveAnim(CardBase card)
    {
        CardObjectBase cardObject = Singleton<DynamicLibrary>.Instance.GetCardObject(card);
        if (cardObject)
        {
            cardObject.SetDraggable(false);
        }

    }

    private IEnumerator Animating(GameObject cardObject)
    {
        cardObject.SetActive(true);
        cardObject.transform.SetParent(layout, true);
        cardObject.transform.rotation = Quaternion.identity;
        cardObject.transform.localScale = Vector3.one;
        cardObject.GetComponent<CardObjectBase>().SetDraggable(true);
        yield return null;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("不允许多个HandsArrangement单例");
        }
    }

    private void OnAddAnim(CardBase card)
    {
        CardObjectBase cardObject = DynamicLibrary.Instance.GetCardObject(card);
        AnimationManager.Instance.AddAnimation(Animating(cardObject.gameObject));
    }

    private void OnDisable()
    {
        OnAdd.RemoveListener(OnAddAnim);
        OnRemove.RemoveListener(OnRemoveAnim);
    }

    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
        OnRemove.AddListener(OnRemoveAnim);
    }
}