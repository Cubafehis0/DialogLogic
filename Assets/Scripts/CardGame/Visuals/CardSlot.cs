using UnityEngine;
using UnityEngine.EventSystems;
public class CardSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardObject c = eventData.pointerDrag.GetComponent<CardObject>();
            if (c)
            {
                CardGameManager.Instance.playerState.CardController.PlayCard(c.Card);
            }
        }
    }
}
