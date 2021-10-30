using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public  Image image;
    public Text text;

    protected DialogTextLoad dialogTextLoad;
    public bool hasPlayEnd;
    public bool canChoose = true;
    //public bool hasDestroyed;

    public ButtonController m_buttonController;

    private void Awake()
    {
    }
    public virtual void ButtonInit(ButtonController buttonController)
    {
        text = this.GetComponentInChildren<Text>();
        image = this.GetComponent<Image>();
        m_buttonController = buttonController;
    }

    //play true ÔòÖð×Ö²¥·Å
    public virtual void SetText(string Button_text,bool play)
    {
        if (text != null)
            this.text.text = Button_text;
    }
    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    public abstract void OnPointerClick(PointerEventData eventData);

}
