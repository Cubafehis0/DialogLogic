using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public interface IDialogController
{
    /// <summary>
    /// 点击DialogPanel上的对话按钮后调用这个
    /// </summary>
    void ClickDialogPanel();

    /// <summary>
    /// 点击DialogChoosePanel上的选择按钮后调用这个
    /// </summary>
    /// <param name="choice"></param>
    void ClickDialogChoosePanel(Choice choice);

    /// <summary>
    /// 点击DialogNarratagePanel上的对话按钮后调用这个
    /// </summary>
    void ClickDialogNarratagePanel();

}

public class DialogController : MonoBehaviour, IDialogController
{
    private InkStory inkUnity;
    [SerializeField] private TextAsset textFile;

    public DialogPanel m_dialogPanel;
    public DialogChoosePanel m_dialogChoosePanel;
    public DialogNarratagePanel m_dialogNarratagePanel;
    public DialogSaveAndLoadPanel m_dialogSaveAndLoadPanel;

    public ButtonController m_buttonController;
    [SerializeField] private bool canUseDialogPanel;
    [SerializeField] private bool canUseDialogChoosePanel;
    [SerializeField] private bool canUseDialogNarratagePanel;
    [SerializeField] private bool canContinue = true;
    [SerializeField] private bool locked = false;
    private Content lastContent = null;

    public void ClickDialogPanel()
    {
        canUseDialogPanel = true;
        canContinue = true;
    }

    public void ClickDialogChoosePanel(Choice choice)
    {
        canUseDialogChoosePanel = true;
        canContinue = true;
        //Debug.Log(choice.index);
        m_dialogSaveAndLoadPanel.SaveTextToFile(choice.content, true);
        Content content = inkUnity.SelectChoice(choice.index);
        lastContent = SetContentToDialog(content);
    }

    public void ClickDialogNarratagePanel()
    {
        canUseDialogNarratagePanel = true;
        canContinue = true;
    }

    public void ClickDialogSaveAndLoadPanel()
    {
        locked = !locked;
    }

    private void Start()
    {
        canUseDialogChoosePanel = canUseDialogNarratagePanel = canUseDialogPanel = true;
        inkUnity = new InkStory(textFile);
        m_dialogPanel = this.transform.Find("DialogPanel").GetComponent<DialogPanel>();
        m_dialogChoosePanel = this.transform.Find("DialogChoosePanel").GetComponent<DialogChoosePanel>();
        m_dialogNarratagePanel = this.transform.Find("DialogNarratagePanel").GetComponent<DialogNarratagePanel>();
        m_dialogSaveAndLoadPanel = this.transform.Find("DialogSaveAndLoadPanel").GetComponent<DialogSaveAndLoadPanel>();
        m_buttonController = this.GetComponent<ButtonController>();
    }

    private void Update()
    {
        //if (m_dialogPanel == null)
        //{
        //    Debug.Log("null");
        //}
        //if (m_dialogChoosePanel == null)
        //{
        //    Debug.Log("null");
        //}
        //if (m_dialogNarratagePanel == null)
        //{
        //    Debug.Log("null");
        //}
        //if (m_buttonController == null)
        //{
        //    Debug.Log("null");
        //}
        //if (locked) return;
        if (inkUnity.CanCoutinue && canContinue)
        {
            canContinue = false;
            Content content = inkUnity.CurrentContent();
            List<Choice> choices = inkUnity.CurrentChoices();
            //if (lastContent != content)
            //{
            //    SetContentToDialog(content);
            //}
            if (ActiveChildCount(m_dialogNarratagePanel.gameObject) == 0 && content.speaker == Speaker.Dialogue)
            {
                lastContent = SetContentToDialog(content);
            }
            else if (lastContent != null && lastContent.speaker != content.speaker)
            {
                lastContent = SetContentToDialog(content);
            }
            //else if (lastContent.richText != content.richText)
            //{
            //    lastContent = SetContentToDialog(content);
            //}
            else
            {
                Debug.Log(ActiveChildCount(m_dialogNarratagePanel.gameObject));
                Debug.Log("no print" + content.speaker + ":" + content.richText);
                Debug.Log("lastContent == content");
            }
            if (choices != null)
            {
                m_dialogChoosePanel.ShowChooseButtons(choices);
            }
        }


    }

    private int ActiveChildCount(GameObject parent)
    {
        var childCount = parent.transform.childCount;
        var activeChildCount = 0;
        for (int i = 0; i < childCount; i++)
        {
            if (parent.transform.GetChild(i).gameObject.activeSelf)
            {
                activeChildCount++;
            }
        }
        return activeChildCount;
    }

    private Content SetContentToDialog(Content content)
    {
        Debug.Log(content.speaker + content.richText);
        m_dialogSaveAndLoadPanel.SaveTextToFile(content, false);
        if (content.speaker == Speaker.Dialogue/* && canUseDialogNarratagePanel*/)
        {
            m_dialogNarratagePanel.SetContent(content);
            canUseDialogNarratagePanel = false;
            return content;

        }
        else if ((content.speaker == Speaker.Player || content.speaker == Speaker.NPC) /*&& canUseDialogPanel*/)
        {
            m_dialogPanel.SetContent(content);
            canUseDialogPanel = false;
            return content;
        }
        else if (!(content.speaker == Speaker.Player || content.speaker == Speaker.NPC || content.speaker == Speaker.Dialogue))
        {
            Debug.LogError("wrong speakerType");
        }
        else
        {
            Debug.Log("no output");
            Debug.Log("canContinue:" + canContinue + " canUseDialogNarratagePanel：" + canUseDialogNarratagePanel + " canUseDialogPanel:" + canUseDialogPanel);
        }
        return null;
    }
}