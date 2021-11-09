using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public interface IButtonScript
{
    /// <summary>
    /// 刷新按钮的部分属性
    /// </summary>
    void RefreshButton();

    /// <summary>
    /// 初始化按钮，得到按钮的部分组件
    /// </summary>
    /// <param name="buttonController"></param>
    void ButtonInit(ButtonController buttonController);

    /// <summary>
    /// 为按钮设置文本
    /// </summary>
    /// <param name="Button_text"></param>
    /// <param name="play"></param>
    void SetText(string Button_text, bool play);
}
public abstract class ButtonScript : MonoBehaviour, IButtonScript, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;
    public Text text;

    protected DialogTextLoad dialogTextLoad;
    public bool hasPlayEnd;
    public bool canChoose = true;
    //public bool hasDestroyed;

    public ButtonController m_buttonController;

    public virtual void RefreshButton()
    {
        text.text = null;
        hasPlayEnd = false;
    }
    public virtual void ButtonInit(ButtonController buttonController)
    {
        text = this.GetComponentInChildren<Text>();
        image = this.GetComponent<Image>();
        m_buttonController = buttonController;
    }

    //play true 则逐字播放
    public virtual void SetText(string Button_text, bool play)
    {
        if (text != null)
        {
            this.text.text = Button_text;
            hasPlayEnd = true;
        }
        else Debug.LogError(text + " is null");
    }
    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    public abstract void OnPointerClick(PointerEventData eventData);

}
