using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class CardObjectBase:MonoBehaviour
{
    [SerializeField]
    protected CardBase card = null;
    [SerializeField]
    private Canvas cardUICanvas = null;
    [SerializeField]
    private int orderInLayer = 0;

    public int OrderInLayer
    {
        get => orderInLayer;
        set
        {
            if (cardUICanvas)
            {
                cardUICanvas.overrideSorting = value > 0;
                cardUICanvas.sortingOrder = value;
            }
            orderInLayer = value;
        }
    }

    public T GetCard<T>() where T : CardBase
    {
        if (card is T res)
        {
            return res;
        }
        else
        {
            return null;
        }
    }

    public void SetCard(CardBase value)
    {
        card = value;
        UpdateVisuals();
    }

    public void SetDraggable(bool value)
    {
        if (!TryGetComponent<Draggable>(out var c)) 
            c = gameObject.AddComponent<Draggable>();
        c.enabled = value;
    }
    public virtual void UpdateVisuals() { }

    public void Awake()
    {
        cardUICanvas = GetComponent<Canvas>();
    }
}