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
            GameObject o = DynamicLibrary.Instance.GetCardObject(card);
            o.GetComponent<Draggable>().enabled = value;
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
            GameObject o = DynamicLibrary.Instance.GetCardObject(card);
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
        GameObject cardObject = DynamicLibrary.Instance.GetCardObject(card);
        if (cardObject)
        {
            cardObject.GetComponent<Draggable>().enabled = false;
        }

    }

    private IEnumerator Animating(GameObject cardObject)
    {
        cardObject.SetActive(true);
        cardObject.transform.SetParent(layout, true);
        cardObject.transform.rotation = Quaternion.identity;
        cardObject.transform.localScale = Vector3.one;
        cardObject.GetComponent<Draggable>().enabled = true;
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
        GameObject cardObject = DynamicLibrary.Instance.GetCardObject(card);
        AnimationManager.Instance.AddAnimation(Animating(cardObject));
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