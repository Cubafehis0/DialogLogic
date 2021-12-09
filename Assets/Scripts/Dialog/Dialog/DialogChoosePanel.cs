using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;
public interface IDialogChoosePanel
{
    IDialogChoosePanel Instance { get; }

    /// <summary>
    /// 传递choice如果是当前故事所有choice（不管是否能选择）中的一个则相当于选择了该选项，返回true，否则返回false
    /// </summary>
    /// <param name="choice"></param>
    /// <returns></returns>
    bool SelectChoice(Choice choice);


    /// <summary>
    /// 关闭选择面板
    /// </summary>
    void Close();

    /// <summary>
    /// 显示选项
    /// </summary>
    /// <param name="choices"></param>
    void Open(List<Choice> choices);

    /// <summary>
    /// 在可选择的choice中加入choice，如果合法，即是所有choice中的一个时
    /// </summary>
    /// <param name="choice"></param>
    void Add(Choice choice);

    /// <summary>
    /// 更新可选择choice
    /// </summary>
    void UpdateChoice();
}

public class DialogChoosePanel : Dialog, IDialogChoosePanel
{
    private GameObject chooseButton1;
    private GameObject chooseButton2;
    private GameObject lastButton;
    private GameObject nextButton;

    private int index;
    private int numOfChoices;
    private List<Choice> nowChoices;
    private List<Choice> allChoices;
    [SerializeField] private Sprite[] spritesOfButtonA;

    public IDialogChoosePanel Instance { get; }

    protected override void HideChildren()
    {
        index = 0;
        numOfChoices = 0;
        nowChoices = null;
        base.HideChildren();
    }

    public void Open(List<Choice> choices)
    {
        RefreshDialogChoosePanel();
        index = 0;
        allChoices = choices;
        nowChoices = ChooseVisiableChoices(choices);
        Debug.Log("nowChoices: " + nowChoices.Count);
        numOfChoices = nowChoices.Count;
        SetChooseButtons(nowChoices);
        CardGameNotion cardGameNotion = new CardGameNotion();
        cardGameNotion.SendStartNotionToCardGame();
    }

    /// <summary>
    /// 开始获取物体
    /// </summary>
    private void Start()
    {
        m_dialogController = this.GetComponentInParent<DialogController>();

        chooseButton1 = this.transform.Find("ChooseButton1").gameObject;
        chooseButton2 = this.transform.Find("ChooseButton2").gameObject;
        lastButton = this.transform.Find("Up").gameObject;
        nextButton = this.transform.Find("Down").gameObject;
        lastButton.GetComponent<Button>().onClick.AddListener(delegate { ClickLastButton(); });
        nextButton.GetComponent<Button>().onClick.AddListener(delegate { ClickNextButton(); });
        HideChildren();
    }

    private List<Choice> ChooseVisiableChoices(List<Choice> choices)
    {
        List<Choice> visiableChoices = new List<Choice>();
        foreach (Choice choice in choices)
        {
            //if (choice.isVisible)
            //{
            visiableChoices.Add(choice);
            //}
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
                //Debug.Log(i);
            }
            else ShowButton(choices[i], chooseButton2.GetComponent<Button>());
        }
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
        buttonScript.ButtonInit(m_dialogController.m_buttonController);
        buttonScript.SetText(choice.content.richText, false);
        buttonScript.image.sprite = spritesOfButtonA[ChooseSprite(choice.BgColor)];
        if (button && !choice.isLocked)
        {
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
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
        HideChildren();
        Debug.Log(choice.content.speaker + ": " + choice.content.richText);
        m_dialogController.ClickDialogChoosePanel(choice);
    }

    private void ClickLastButton()
    {
        Debug.Log(index);
        Debug.Log(numOfChoices);
        if (index != 0)
        {
            Debug.Log(index);
            RefreshDialogChoosePanel();
            index -= 2;
            SetChooseButtons(nowChoices);
        }
    }
    private void ClickNextButton()
    {
        Debug.Log("click nextbutton1");
        Debug.Log(index != numOfChoices - 1);
        Debug.Log(index);
        Debug.Log(numOfChoices);
        Debug.Log("click nextbutton");
        RefreshDialogChoosePanel();
        index += 2;
        SetChooseButtons(nowChoices);
    }

    private void ShowNextOrLastButton(bool last, bool next)
    {
        lastButton.SetActive(last);
        nextButton.SetActive(next);
    }

    private void RefreshDialogChoosePanel()
    {
        var childCount = this.transform.childCount;
        chooseButton1.GetComponent<Button>().onClick.RemoveAllListeners();
        if (index + 1 < numOfChoices)
        {
            chooseButton2.GetComponent<Button>().onClick.RemoveAllListeners();
        }

        for (int i = 0; i < childCount; i++)
            this.transform.GetChild(i).gameObject.SetActive(false);

    }

    public void Close()
    {
        HideChildren();
    }

    public void UpdateChoice()
    {
        m_dialogController.UpdateChooseDialogPanel();
    }

    public bool SelectChoice(Choice choice)
    {
        foreach (Choice c in allChoices)
        {
            if (c == choice)
            {
                OnClickChoiceButton(choice, null);
                return true;
            }
        }
        return false;
    }

    public void Add(Choice choice)
    {
        foreach (Choice c in allChoices)
        {
            if (c == choice)
            {
                nowChoices.Add(choice);
                int i = index;
                RefreshDialogChoosePanel();
                index = i;
                SetChooseButtons(nowChoices);
            }
        }
    }
}
