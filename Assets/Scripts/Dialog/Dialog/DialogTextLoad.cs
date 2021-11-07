using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Ink2Unity;

public class DialogTextLoad : MonoBehaviour
{

    public DialogTextSave m_dialogTextSave;
    public ButtonController m_buttonController;
    public TextAsset textFile;
    private Transform DialogPanel;
    public Transform DialogChoosePanel;
    public Transform DialogNarratagePanel;

    public InkStory inkUnity;
    /* UI Prefabs */
    // 文本
    // 按钮
    [SerializeField] private Text text;
    [SerializeField] private Button button;

    // 初始化
    private void Awake()
    {
        inkUnity = new InkStory(textFile);
        GameObject Dialog = GameObject.Find("Dialog").gameObject;
        DialogChoosePanel = Dialog.transform.Find("DialogChoosePanel");
        DialogPanel = Dialog.transform.Find("DialogPanel");
        DialogNarratagePanel = Dialog.transform.Find("DialogNarratagePanel");
        m_buttonController = this.transform.GetComponentInParent<ButtonController>();
        m_dialogTextSave = this.transform.parent.transform.GetComponentInChildren<DialogTextSave>();
    }
    public void UpdateChoose()
    {
        HideChildren(DialogChoosePanel.gameObject);
        inkUnity.UpdateInk();
        ShowDialogChoose();
    }
    public void BeginDialog()
    {
        // 清空画布内容
        //RemoveChildren(DialogChoosePanel.gameObject);
        //ShowDialog();
        //Debug.Log(DialogPanel.childCount + "   " + DialogNarratagePanel.childCount);
        if (DialogNarratagePanel.childCount == 0)
        {
            //Debug.Log("_inkStory.canContinue");
            Button button = ShowDialog();
            if (inkUnity.CanCoutinue)
            {
                button.onClick.AddListener(delegate { OnClickDialogButton(button); });
            }

        }

        //Debug.Log("_inkStory.currentChoices.Count:" + inkUnity.CurrentChoices().Count);

        ShowDialogChoose();

    }

    public Button ShowDialog()
    {
        //RemoveChildren(DialogPanel.gameObject);
        //RemoveChildren(DialogNarratagePanel.gameObject);
        if (inkUnity.CanCoutinue)
        {
            //Debug.Log("can continue"); 

            Content content = inkUnity.CurrentContent();

            GameObject storyButton = m_buttonController.CreateButtonB(content);

            if (storyButton == null) Debug.Log("no storyButton");

            m_dialogTextSave.SaveTextToFile(m_dialogTextSave.textSaveFile, content);

            return storyButton.GetComponent<Button>();
        }
        //else Debug.Log("cannot continue");

        return null;
    }
    private void ShowDialogChoose()
    {
        List<Choice> choices = inkUnity.CurrentChoices();
        if (choices.Count > 0 && DialogChoosePanel.childCount == 0)
        {
            DialogNarratagePanel.gameObject.SetActive(false);
            //Debug.Log("大大大于0啦");
            for (var i = 0; i < choices.Count; i++)
            {
                //Debug.Log("大大大于0啦");

                var choice = choices[i];

                GameObject choiceButton = m_buttonController.CreateButtonA(choice);

                // 获取按钮点击后对应的路径
                if (choiceButton && !choice.isLocked)
                {
                    choiceButton.GetComponent<Button>().onClick.AddListener(delegate{
                    OnClickChoiceButton(choice, choiceButton);});
                }
                else if (choiceButton && choice.isLocked)
                {
                    Debug.Log("isLocked");
                    choiceButton.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Debug.Log("大大大于0啦");
                }
            }
        }
    }



    public void OnClickDialogButton(Button button)
    {
        if (button.GetComponent<ButtonScript>().hasPlayEnd)
        {
            RemoveChildren(DialogPanel.gameObject);
            RemoveChildren(DialogNarratagePanel.gameObject);
        }
    }
    public void HideChildren(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            parent.transform.GetChild(i).gameObject.SetActive(false);

    }
    public void HideChildren(GameObject parent, GameObject gameObject)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject != gameObject)
                parent.transform.GetChild(i).gameObject.SetActive(false);

        }
    }
    public void RemoveChildren(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
            Destroy(parent.transform.GetChild(i).gameObject);
    }
    public void RemoveChildren(GameObject parent, GameObject gameObject)
    {
        var childCount = parent.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject != gameObject)
                Destroy(parent.transform.GetChild(i).gameObject);

        }
    }
    public void OnClickChoiceButton(Choice choice, GameObject button)
    {
        DialogNarratagePanel.gameObject.SetActive(true);
        HideChildren(DialogChoosePanel.gameObject, button);
        inkUnity.SelectChoice(choice);
        m_dialogTextSave.SaveTextToFile(m_dialogTextSave.textSaveFile, choice.content); 
        HideChildren(DialogPanel.gameObject);
        HideChildren(DialogNarratagePanel.gameObject);
        //ShowDialog();
    }

}
