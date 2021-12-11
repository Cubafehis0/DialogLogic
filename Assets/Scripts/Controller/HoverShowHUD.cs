using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHUD
{
    void Show();

    void Close();
}

[RequireComponent(typeof(Card))]
public class HoverShowHUD : MonoBehaviour, IHUD
{
    [SerializeField]
    private float timer;

    public UnityEvent OnTimeUp = new UnityEvent();

    private float t = 0f;

    private Card card = null;

    private void Awake()
    {
        card = GetComponent<Card>();
    }

    public void Show()
    {
        //CardDescHUD.instance.CreateCardDesc(card.staticID.ToString(), transform.position + new Vector3(1f, 1f, 0f));
    }

    public void Close()
    {
        if (CardDescHUD.instance)
            CardDescHUD.instance.DestroyCardDesc();
    }

    private void Update()
    {
        if (Mathf.Abs(t) > 0.05f)
        {
            t -= Time.deltaTime;
            if (Mathf.Abs(t) < 0.05f)
            {
                t = 0.0f;
                Show();
            }
        }
    }

    public void OnMouseEnter()
    {
        t = timer;
    }

    public void OnMouseExit()
    {
        t = 0f;
        Close();
    }
    private void OnDisable()
    {
        Close();
    }
}
