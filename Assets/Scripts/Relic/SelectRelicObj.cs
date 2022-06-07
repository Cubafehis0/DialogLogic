using UnityEngine.EventSystems;

public class SelectRelicObj : RelicObj, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.selectRelicUI.OnChooseRelic(relic);
    }
}