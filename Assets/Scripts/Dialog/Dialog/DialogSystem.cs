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
    [SerializeField]
    private TextAsset textAsset = null;

    private static DialogSystem instance = null;
    public static DialogSystem Instance { get => instance; }

    private InkStory inkStory;

    private void Awake()
    {
        instance = this;
    }

    public static void Pause()
    {
        Debug.LogWarning("��ͣδʵ��");
    }
    public void SetInkStoryAsset(string name)
    {
        string InkStoryPath = Path.Combine(Application.dataPath, "InkStory");
        if (!Directory.Exists(InkStoryPath))
        {
            Debug.LogError("AssetĿ¼�²�����InkStory�ļ�");
            return;
        }
        string[] filePath = Directory.GetFiles(InkStoryPath, name+".json", SearchOption.AllDirectories);
        if (filePath.Length <= 0)
        {
            Debug.LogError($"InkStoryĿ¼�²�������Ϊ{name}���ļ�");
            return;
        }
        textAsset = new TextAsset(File.ReadAllText(filePath[0]));
    }
    private void Start()
    {
        NPC_Dialog.OnClick.AddListener(OnClickDialogButton);
        Player_Dialog.OnClick.AddListener(OnClickDialogButton);
        narratageDialogButton.OnClick.AddListener(OnClickDialogButton);
        if (textAsset)
        {
            inkStory = new InkStory(textAsset);
            if (AutoPlay) MoveNext();
        }
    }
    private void OnClickDialogButton(RichButton button)
    {
        if (AutoPlay) MoveNext();
    }

    /// <summary>
    /// ���ж��Ƿ��ѡ��ǿ���ж��ɹ�/ʧ��
    /// </summary>
    /// <param name="choice"></param>
    /// <param name="success"></param>
    public void ForceSelectChoice(Choice choice, bool success)
    {
        ChooseSystem.Instance.Close();
        CardGameManager.Instance.EndTurn();
        DialogSaveAndLoadPanel.Instance.SaveTextToFile(choice.Content, true);
        inkStory.SelectChoice(choice, success);
        MoveNext();
    }


    public void MoveNext()
    {
        if (inkStory.NextState == InkState.Content)
        {
            Content content = inkStory.NextContent();
            SpeakSystem.Instance.Speak(content.richText, content.speaker);
            DialogSaveAndLoadPanel.Instance.SaveTextToFile(content);
            AutoPlay = true;
        }
        else
        {
            List<Choice> choices = inkStory.CurrentChoices();
            if (choices != null && choices.Count != 0)
            {
                ChooseSystem.Instance.Init(choices);
                ChooseSystem.Instance.Open();
                CardGameManager.Instance.StartTurn();
                AutoPlay = false;
            }
        }
    }
}