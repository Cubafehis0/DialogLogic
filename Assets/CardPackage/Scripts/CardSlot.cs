using UnityEngine;
using UnityEngine.EventSystems;
public class CardSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private CardController cardController;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardObjectBase c = eventData.pointerDrag.GetComponent<CardObjectBase>();
            if (c)
            {
                cardController.PlayCard(c.GetCard<CardBase>());
            }
        }
    }
}
