using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;
public interface IDialogChoosePanel
{
    /// <summary>
    /// 隐藏物体的所有子物体，重置一些变量，gameobject 确定为 DialogChoosePanel
    /// </summary>
    /// <param name="gameObject"></param>
    void HideChildren(GameObject gameObject);

    /// <summary>
    /// 调用显示选择选项
    /// </summary>
    /// <param name="choices"></param>
    void ShowChooseButtons(List<Choice> choices);

}
public class DialogChoosePanel : Dialog, IDialogChoosePanel
{
    public DialogController m_dialogController;

    private GameObject chooseButton1;
    private GameObject chooseButton2;
    private GameObject lastButton;
    private GameObject nextButton;

    private int index;
    private int numOfChoices;
    private List<Choice> nowChoices;
    [SerializeField] private Sprite[] spritesOfButtonA;
    

    public override void HideChildren(GameObject parent)
    { 
        index = 0;
        numOfChoices = 0;
        nowChoices = null;
        lastButton.GetComponent<Button>().onClick.RemoveListener(delegate { ClickLastButton(); });
        nextButton.GetComponent<Button>().onClick.RemoveListener(delegate { ClickNextButton(); });
        base.HideChildren(parent);
    }

    public void ShowChooseButtons(List<Choice> choices)
    {
        index = 0;
        nowChoices = ChooseVisiableChoices(choices);
        numOfChoices = nowChoices.Count;
        lastButton.GetComponent<Button>().onClick.AddListener(delegate { ClickLastButton(); });
        nextButton.GetComponent<Button>().onClick.AddListener(delegate { ClickNextButton(); });

        SetChooseButtons(nowChoices);
    }
    
    /// <summary>
    /// 开始获取物体
    /// </summary>
    private void Awake()
    {
        m_dialogController = this.GetComponentInParent<DialogController>();

        chooseButton1 = this.transform.Find("ButtonA1").gameObject;
        chooseButton2 = this.transform.Find("ButtonA2").gameObject;
        lastButton = this.transform.Find("Up").gameObject;
        nextButton = this.transform.Find("Down").gameObject;
        //lastButton.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        //nextButton.GetComponent<ButtonScript>().ButtonInit(m_dialogController.m_buttonController);
        HideChildren(this.gameObject);
    }

    private List<Choice> ChooseVisiableChoices(List<Choice> choices)
    {
        List<Choice> visiableChoices = new List<Choice>();
        foreach (Choice choice in choices)
        {
            if (choice.isVisible)
            {
                visiableChoices.Add(choice);
            }
        }
        return visiableChoices;
    }
    private void SetChooseButtons(List<Choice> choices)
    {
        if (choices.Count > 2)
        {
            bool last, next;
            last = index != 0;
            next = index + 2 < numOfChoices;
            ShowNextOrLastButton(last, next);
        }
        for (int i = index; i < choices.Count && i < index + 2; i++)
        {
            if (i == index)
            {
                ShowButton(choices[i], chooseButton1.GetComponent<Button>());
            }
            else ShowButton(choices[i], chooseButton2.GetComponent<Button>());
        }
        index = index + 2 > choices.Count ? choices.Count - 1 : index + 2;
    }
    private int ChooseSprite(Color color)
    {
        if (color == Color.yellow)
        {
            return 0;
        }
        else if (color == Color.green)
        {
            return 1;
        }
        else if (color == Color.red)
        {
            return 2;
        }
        else if (color == Color.white)
        {
            return 3;
        }
        return 0;
    }

    private void ShowButton(Choice choice, Button button)
    {
        button.gameObject.SetActive(true);
        ButtonScript buttonScript = button.GetComponent<ButtonScript>();
        buttonScript.ButtonInit(this.m_dialogController.m_buttonController);
        buttonScript.SetText(choice.content.richText, false);
        buttonScript.image.sprite = spritesOfButtonA[ChooseSprite(choice.BgColor)];
        if (button && !choice.isLocked)
        {
            button.GetComponent<Button>().onClick.AddListener(delegate {
                OnClickChoiceButton(choice, button.gameObject);
            });
        }
        else if (button && choice.isLocked)
        {
            Debug.Log("isLocked");
            button.GetComponent<Button>().interactable = false;
        }
        else
        {
            Debug.Log("大大大于0啦");
        }

    }
    private void OnClickChoiceButton(Choice choice, GameObject button)
    {
        m_dialogController.ClickDialogChoosePanel(choice);
    }

    private void ClickLastButton()
    {
        if (index != 0)
        {
            HideChildren(this.gameObject);
            index -= 2;
            SetChooseButtons(nowChoices);
        }
    }
    private void ClickNextButton()
    {
        if (index != numOfChoices - 1)
        {
            HideChildren(this.gameObject);
            index += 2;
            SetChooseButtons(nowChoices);
        }
    }

    private void ShowNextOrLastButton(bool last, bool next)
    {
        lastButton.SetActive(last);
        nextButton.SetActive(next);
    }
}
