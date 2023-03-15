using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : CardSlotBase<Card> { }
public class CardSlotBase<T> : MonoBehaviour, IDropHandler where T : CardBase
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardObjectBase<T> c = eventData.pointerDrag.GetComponent<CardObjectBase<T>>();
            if (c)
            {
                CardController.Instance.PlayCard(c.GetCard(), gameObject);
            }
        }
    }
}
