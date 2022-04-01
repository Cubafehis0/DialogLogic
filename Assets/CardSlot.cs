using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CardSlot : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var dragCard = eventData.pointerDrag.gameObject;
            Card card =dragCard.GetComponent<Card>();
            if (card)
            {
                float y = dragCard.transform.localPosition.y;
                Debug.Log(y);
                if(y>350)
                    CardPlayerState.Instance.PlayCard(card);
            }
        }
    }
}
