using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ink.Runtime;

public interface IButtonB
{
    /// <summary>
    /// 初始化button，得到button部分组件，继承ButtonScript
    /// </summary>
    /// <param name="buttonController"></param>
    void ButtonInit(ButtonController buttonController);

    /// <summary>
    /// 重置button部分属性，继承ButtonScript
    /// </summary>
    void RefreshButton();

    /// <summary>
    /// 为button设置文本，play为true则调用协程打印文本，false则直接赋值文本，继承ButtonScript
    /// </summary>
    /// <param name="Button_text"></param>
    /// <param name="play"></param>
    void SetText(string Button_text, bool play);

}
public class ButtonB : ButtonScript, IButtonB
{

    private string text_;

    public override void ButtonInit(ButtonController buttonController)
    {
        base.ButtonInit(buttonController);
        hasPlayEnd = false;
    }

    public override void RefreshButton()
    {
        hasPlayEnd = false;
        text.text = null;
    }

    public override void SetText(string Button_text, bool play)
    {
        //Debug.Log(Button_text + " play: " + play + " hasPlayEnd: " + hasPlayEnd);
        if (play)
            StartCoroutine(ShowText(Button_text));
        else
            base.SetText(Button_text, play);
    }
    IEnumerator ShowText(string Button_text)
    {
        text_ = Button_text;

        for (int i = 0; i < Button_text.Length; i++)
        {
            if (hasPlayEnd == false && Button_text[i] != '\r' && Button_text[i] != '\n')
                text.text += Button_text[i];
            if (i == Button_text.Length - 1)
                hasPlayEnd = true;
            yield return new WaitForSeconds(.1f);
        }

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (hasPlayEnd)
        {

        }
        else
        {
            text.text = text_;
            hasPlayEnd = true;
        }
        //if (m_buttonController.dialogTextLoad)
        //    m_buttonController.dialogTextLoad._storyNeeded = true;
        //else Debug.Log("no m_buttonController.dialogTextLoad");
        //Debug.Log("onclickbuttonB");
        //Destroy(this.gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("enter");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }


}
