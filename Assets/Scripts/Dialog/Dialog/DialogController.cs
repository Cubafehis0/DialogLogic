using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public interface IDialogController
{

    void SetContent(Content c);

    /// <summary>
    /// 
    /// </summary>
    void ContinueDialog();

}

public class DialogController : MonoBehaviour, IDialogController
{
    

    public ContentPanel m_contentPanel;
    public DialogChoosePanel m_dialogChoosePanel;
    public DialogSaveAndLoadPanel m_dialogSaveAndLoadPanel;

    private GameObject dialogNarratagePanel;
    private GameObject dialogChoosePanel;
    public ButtonController m_buttonController;
    [SerializeField] private bool canUseDialogPanel;
    [SerializeField] public bool canUseDialogChoosePanel;
    [SerializeField] private bool canUseDialogNarratagePanel;
    [SerializeField] public bool canContinue = true;
    [SerializeField] private bool locked = false;
    [SerializeField] public InkStory inkStory;
    private Content lastContent = null;


    public void ContinueDialog()
    {
        if (m_buttonController.buttonB)
        {
            m_buttonController.buttonB.SetText();
        }
        else update();
    }
    public void ClickDialogPanel()
    {
        canUseDialogPanel = true;
        canContinue = true;
        update();
    }
    
    public void ClickDialogChoosePanel(Choice choice)
    {
        Debug.Log(choice.index);
        m_dialogSaveAndLoadPanel.SaveTextToFile(choice.content, true);
        Content content = inkStory.SelectChoice(choice.index);
        lastContent = SetContentToDialog(content);
        update();
        canContinue = true;
    }

    public void ClickDialogNarratagePanel()
    {
        canUseDialogNarratagePanel = true;
        canContinue = true;
        update();
    }

    public void ClickDialogSaveAndLoadPanel()
    {
        locked = !locked;
    }
    public void UpdateChooseDialogPanel()
    {
        List<Choice> choices = inkStory.CurrentChoices();
        m_dialogChoosePanel.Open(choices);
    }

    private void Start()
    {
        canUseDialogChoosePanel = canUseDialogNarratagePanel = canUseDialogPanel = true;
        dialogNarratagePanel = this.transform.Find("DialogNarratagePanel").gameObject;
        dialogChoosePanel = this.transform.Find("DialogChoosePanel").gameObject;
        m_contentPanel = this.transform.Find("DialogPanel").GetComponent<ContentPanel>();
        m_dialogChoosePanel = this.transform.Find("DialogChoosePanel").GetComponent<DialogChoosePanel>();
        m_dialogSaveAndLoadPanel = this.transform.Find("DialogSaveAndLoadPanel").GetComponent<DialogSaveAndLoadPanel>();
        m_buttonController = this.GetComponent<ButtonController>();
        update();
    }

    private void Update()
    {
        if (ActiveChildCount(dialogChoosePanel) == 0)
        {
            canUseDialogChoosePanel = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("update");
            ContinueDialog();
        }
    }
    public void update()
    {
        canContinue = true;
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
        //Debug.Log("active" + childCount+" "+activeChildCount+ parent.transform.GetChild(0).gameObject.name);
        return activeChildCount;
    }

    private Content SetContentToDialog(Content content)
    {
        canContinue = false;
        Debug.Log(content.speaker + content.richText);
        m_dialogSaveAndLoadPanel.SaveTextToFile(content, false);
        if ((content.speaker == Speaker.Player || content.speaker == Speaker.NPC||content.speaker==Speaker.Dialogue))
        {
            m_contentPanel.SetContent(content.richText, content.speaker);
            if (content.speaker == Speaker.Dialogue) canUseDialogNarratagePanel = false;
            else canUseDialogPanel = false;
            return content;
        }
        else if (!(content.speaker == Speaker.Player || content.speaker == Speaker.NPC || content.speaker == Speaker.Dialogue))
        {
            Debug.LogError("wrong speakerType");
        }
        else
        {
            Debug.Log("no output");
            Debug.Log("canContinue:" + canContinue + " canUseDialogNarratagePanel£º" + canUseDialogNarratagePanel + " canUseDialogPanel:" + canUseDialogPanel);
        }
        return null;
    }
    public void SetChoice(List<Choice> choices)
    {
        canUseDialogChoosePanel = false;
        m_dialogChoosePanel.Open(choices);
    }

    public void SetContent(Content c)
    {
        if (c.speaker == Speaker.Dialogue)
        {
            lastContent = SetContentToDialog(c);
        }
        else if (lastContent != null && lastContent.speaker != c.speaker)
        {
            lastContent = SetContentToDialog(c);
        }
        else
        {
            Debug.Log(ActiveChildCount(dialogNarratagePanel));
            Debug.Log("no print" + c.speaker + ":" + c.richText);
            Debug.Log("lastContent == content");
        }
    }
}