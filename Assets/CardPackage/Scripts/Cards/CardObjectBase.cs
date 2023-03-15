using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class CardObjectBase<T>:MonoBehaviour where T : CardBase
{
    [SerializeField]
    protected T card = null;

    public T GetCard() 
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

    public void SetCard(T value)
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

}