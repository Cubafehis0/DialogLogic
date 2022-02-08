using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ISpeakSystem
{
    void Speak(string richText, Speaker speaker);
}

public class SpeakSystem : MonoBehaviour, ISpeakSystem
{
    
    [SerializeField]
    private RichTypeButton NPC_Dialog=null;
    [SerializeField]
    private RichTypeButton Player_Dialog = null;
    [SerializeField]
    private RichTypeButton Narratage_Dialog = null;

    private static ISpeakSystem instance = null;
    public static ISpeakSystem Instance { get => instance; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="richText">说话内容（富文本）</param>
    /// <param name="speaker">说话者</param>
    public void Speak(string richText, Speaker speaker)
    {
        if (speaker == Speaker.NPC)
        {
            ShowNPCDialog(richText);
        }
        else if (speaker == Speaker.Player)
        {
            ShowPlayerDialog(richText);
        }
        else if (speaker == Speaker.Dialogue)
        {
            ShowNarratageDialog(richText);
        }
        else Debug.LogError("不能识别speaker类型");
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    private void CloseAllDialog()
    {
        Narratage_Dialog.gameObject.SetActive(false);
        NPC_Dialog.gameObject.SetActive(false);
        Player_Dialog.gameObject.SetActive(false);
    }

    private void ShowNPCDialog(string richText)
    {
        CloseAllDialog();
        NPC_Dialog.gameObject.SetActive(true);
        NPC_Dialog.TypeText(richText);
    }

    private void ShowPlayerDialog(string richText)
    {
        CloseAllDialog();
        Player_Dialog.gameObject.SetActive(true);
        Player_Dialog.TypeText(richText);
    }

    private void ShowNarratageDialog(string richText)
    {
        CloseAllDialog();
        Narratage_Dialog.gameObject.SetActive(true);
        Narratage_Dialog.TypeText(richText);
    }
}
