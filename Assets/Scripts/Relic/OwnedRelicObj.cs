using UnityEngine.EventSystems;
using UnityEngine;

public class OwnedRelicObj : RelicObj, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CanvasGroup nameCG;
    public CanvasGroup descCG;
    public CanvasGroup rarityCG;
    public CanvasGroup categoryCG;

    public void OnPointerEnter(PointerEventData eventData)
    {
        nameCG.alpha = 0f;
        rarityCG.alpha = 0f;
        categoryCG.alpha = 0f;
        descCG.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameCG.alpha = 1f;
        rarityCG.alpha = 1f;
        categoryCG.alpha = 1f;
        descCG.alpha = 0f;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.selectRelicUI.OnRemoveRelic(relic);
        Destroy(gameObject);
    }
}