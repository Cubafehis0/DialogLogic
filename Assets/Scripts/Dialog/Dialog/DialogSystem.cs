using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDialogSystem
{
    void MoveNext();
}
public class DialogSystem : MonoBehaviour, IDialogSystem
{
    [SerializeField]
    private bool AutoPlay = true;
    [SerializeField]
    private RichTypeButton NPC_Dialog = null;
    [SerializeField]
    private RichTypeButton Player_Dialog = null;
    [SerializeField]
    private RichTypeButton narratageDialogButton = null;

    //[SerializeField]
    //private TextAsset testAsset = null;
    private static DialogSystem instance = null;
    public static DialogSystem Instance { get => instance; }

    private InkStory inkStory;

    private void Awake()
    {
        instance = this;
    }

    public static void Pause()
    {
        Debug.LogWarning("暂停未实现");
    }

    //private void Start()
    //{
    //    NPC_Dialog.OnClick.AddListener(OnClickDialogButton);
    //    Player_Dialog.OnClick.AddListener(OnClickDialogButton);
    //    narratageDialogButton.OnClick.AddListener(OnClickDialogButton);
    //    CreateNewDialog(testAsset);
    //    if (AutoPlay) MoveNext();
    //}

    public void Open(TextAsset textAsset)
    {
        if (textAsset != null)
        {
            NPC_Dialog.OnClick.AddListener(OnClickDialogButton);
            Player_Dialog.OnClick.AddListener(OnClickDialogButton);
            narratageDialogButton.OnClick.AddListener(OnClickDialogButton);
            CreateNewDialog(textAsset);
            if (AutoPlay) MoveNext();
        }
        else
        {
            throw new System.ArgumentNullException();
        }
    }

    public void CreateNewDialog(TextAsset textAsset)
    {
        inkStory = new InkStory(textAsset);
    }

    private void OnClickDialogButton(RichButton button)
    {
        if (AutoPlay) MoveNext();
    }

    /// <summary>
    /// 不判断是否可选，强制判定成功/失败
    /// </summary>
    /// <param name="choice"></param>
    /// <param name="success"></param>
    public void ForceSelectChoice(Choice choice, bool success)
    {
        GUISystemManager.Instance.chooseSystem.Close();
        CardGameManager.Instance.EndTurn();
        //DialogSaveAndLoadPanel.Instance.SaveTextToFile(choice.Content, true);
        inkStory.SelectChoice(choice, success);
        MoveNext();
        AutoPlay = true;
    }


    public void MoveNext()
    {
        if (inkStory.NextState == InkState.Content)
        {
            Content content = inkStory.NextContent(); ;
            SpeakSystem.Instance.Speak(content.richText, content.speaker);
            //DialogSaveAndLoadPanel.Instance.SaveTextToFile(content);
        }
        else
        {
            List<Choice> choices = inkStory.CurrentChoices();
            if (choices != null && choices.Count != 0)
            {
                GUISystemManager.Instance.chooseSystem.Open(choices);
                CardGameManager.Instance.StartTurn();
                AutoPlay = false;
            }
        }
    }
}