using UnityEngine;
using UnityEngine.EventSystems;
public class CardSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardObjectBase c = eventData.pointerDrag.GetComponent<CardObjectBase>();
            if (c)
            {
                CardControllerBase.Instance.PlayCard(c.GetCard<CardBase>(),gameObject);
            }
        }
    }
}
