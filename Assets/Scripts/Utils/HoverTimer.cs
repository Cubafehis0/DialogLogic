using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverTimer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private float timer = 2;
    [SerializeField]
    private float t = 0f;

    public UnityEvent OnTimeUp = new UnityEvent();
    public UnityEvent OnLeave = new UnityEvent();


    private void Update()
    {
        if (Mathf.Abs(t) > 0.05f)
        {

            t -= Time.deltaTime;
            if (Mathf.Abs(t) < 0.05f)
            {
                t = 0.0f;
                OnTimeUp.Invoke();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        t = timer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Mathf.Abs(t) < 0.05f) OnLeave.Invoke();
        t = 0f;
    }
}
