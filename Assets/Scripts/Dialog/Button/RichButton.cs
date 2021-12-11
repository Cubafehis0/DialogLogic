using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ink.Runtime;

public interface IRichButton
{
    void SetSprite(Sprite sprite);
    void SetText(string Button_text);
}

public class RichButton : MonoBehaviour, IRichButton
{
    public Button btn;
    protected Image img;
    protected Text txt;
    public UnityEvent<RichButton> OnClick = new UnityEvent<RichButton>();

    private void Awake()
    {
        btn = GetComponent<Button>();
        txt = GetComponentInChildren<Text>();
        img = GetComponent<Image>();
    }

    protected virtual void Start()
    {
        btn.onClick.AddListener(delegate { OnClick.Invoke(this); });
    }

    public void SetText(string Button_text)
    {
        if (txt != null)
        {
            txt.text = Button_text;
        }
        else
        {
            Debug.LogWarning(name + " 缺少Text组件");
        }
    }

    public void SetSprite(Sprite sprite)
    {
        if (img != null)
        {
            img.sprite = sprite;
        }
        else
        {
            Debug.LogWarning(name + " 缺少Image组件");
        }
    }
}
