using Ink2Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDialogSystem
{
    void MoveNext();
    void SelectChoice(Choice choice);
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
    private TextAsset testAsset=null;

    private static IDialogSystem instance = null;
    public static IDialogSystem Instance { get => instance; }

    private InkStory inkStory;

    private void Awake()
    {
        instance = this;
    }

    public static void Pause()
    {
        Debug.LogWarning("‘›Õ£Œ¥ µœ÷");
    }

    private void Start()
    {
        NPC_Dialog.OnClick.AddListener(OnClickDialogButton);
        Player_Dialog.OnClick.AddListener(OnClickDialogButton);
        narratageDialogButton.OnClick.AddListener(OnClickDialogButton);
        if (testAsset)
        {
            CreateNewDialog(testAsset);
            if(AutoPlay) MoveNext();
        }
    }

    public void CreateNewDialog(TextAsset textAsset)
    {
        inkStory = new InkStory(textAsset);
    }

    private void OnClickDialogButton(RichButton button)
    {
        if(AutoPlay) MoveNext();
    }

    public void SelectChoice(Choice choice)
    {
        //DialogSaveAndLoadPanel.Instance.SaveTextToFile(choice.content, true);
        ChooseSystem.Instance.Close();
        CardGameManager.Instance.EndTurn();
        DialogSaveAndLoadPanel.Instance.SaveTextToFile(choice.content, true);
        inkStory.SelectChoice(choice.index);
        MoveNext();
        AutoPlay = true;
    }

    public void MoveNext()
    {
        if (inkStory.NextState == InkState.Content)
        {
            Content content = inkStory.NextContent(); ;
            SpeakSystem.Instance.Speak(content.richText, content.speaker);
            DialogSaveAndLoadPanel.Instance.SaveTextToFile(content);
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