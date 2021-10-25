using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHUD
{
    void Show();

    void Close();
}

[RequireComponent(typeof(CardObject))]
public class HoverShowHUD : MonoBehaviour,IHUD
{
    [SerializeField]
    private float timer;

    public UnityEvent OnTimeUp=new UnityEvent();

    private float t = 0f;

    private CardObject card = null;

    private void Awake()
    {
        card = GetComponent<CardObject>();
    }

    public void Show()
    {
        CardDescHUD.instance.CreateCardDesc(card.desc, transform.position + new Vector3(1f, 1f, 0f));
    }

    public void Close()
    {
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
