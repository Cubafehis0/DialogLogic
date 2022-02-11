using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;
public interface IChooseSystem:ITable
{
    /// <summary>
    /// ����choice����ǵ�ǰ��������choice�������Ƿ���ѡ���е�һ�����൱��ѡ���˸�ѡ�����true�����򷵻�false
    /// </summary>
    /// <param name="choice"></param>
    /// <returns></returns>
    bool SelectChoice(int index);

    /// <summary>
    /// ��ʾѡ��
    /// </summary>
    /// <param name="choices"></param>
    void Init(List<Choice> choices);

    /// <summary>
    /// �ڿ�ѡ���choice�м���choice������Ϸ�����������choice�е�һ��ʱ
    /// </summary>
    /// <param name="choice"></param>
    void Add(Choice choice);

    /// <summary>
    /// ���¿�ѡ��choice
    /// </summary>
    void UpdateVisualList();

}

public class ChooseSystem : MonoBehaviour, IChooseSystem
{
    [SerializeField]
    private List<RichButton> chooseButtons = new List<RichButton>();
    [SerializeField]
    private Button lastButton = null;
    [SerializeField]
    private Button nextButton = null;
    [SerializeField]
    private Sprite[] spritesOfButtonA = null;

    private int index = 0;
    private List<Choice> visibleChoices = new List<Choice>();
    private List<Choice> allChoices = new List<Choice>();

    private static IChooseSystem instance = null;
    public static IChooseSystem Instance { get => instance; }

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

    private void Start()
    {
        if (lastButton) lastButton.onClick.AddListener(ClickLastButton);
        if (nextButton) nextButton.onClick.AddListener(ClickNextButton);
        foreach (var btn in chooseButtons)
        {
            btn.OnClick.AddListener(ClickChoiceButton);
        }
        Close();
        UpdateVisualList();
    }

    public void Close()
    {
        index = 0;
        allChoices.Clear();
        visibleChoices.Clear();
        gameObject.SetActive(false);
    }

    public void Init(List<Choice> choices)
    {
        if (choices == null) return;
        index = 0;
        allChoices = choices;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        UpdateVisualList();
    }

    public void UpdateVisualList()
    {
        visibleChoices = ChooseVisiableChoices(allChoices);
        if (chooseButtons.Count == 0) return;
        int page = index / chooseButtons.Count;
        int totalPage = Mathf.CeilToInt(1f * visibleChoices.Count / chooseButtons.Count);
        //if (lastButton) lastButton.gameObject.SetActive(page > 0);
        //if (nextButton) nextButton.gameObject.SetActive(page + 1 < totalPage);
        if (lastButton) lastButton.interactable = page > 0 ;
        if (nextButton) nextButton.interactable = page + 1 < totalPage;
        for (int i = 0; i < chooseButtons.Count; i++)
        {
            if (index + i < visibleChoices.Count)
            {
                chooseButtons[i].gameObject.SetActive(true);
                chooseButtons[i].SetText(visibleChoices[index + i].content.richText);
                chooseButtons[i].SetSprite(spritesOfButtonA[ChooseSprite(visibleChoices[index + i].BgColor)]);
                if (visibleChoices[index + i].isLocked)
                {
                    chooseButtons[i].btn.interactable = false;
                }
            }
            else
            {
                chooseButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void ClickLastButton()
    {
        index -= chooseButtons.Count;
        UpdateVisualList();
    }

    private void ClickNextButton()
    {
        index += chooseButtons.Count;
        UpdateVisualList();
    }

    private void ClickChoiceButton(RichButton from)
    {
        SelectChoice(chooseButtons.IndexOf(from) + index);
    }

    public bool SelectChoice(int choiceIndex)
    {
        DialogSystem.Instance.SelectChoice(visibleChoices[choiceIndex]);
        return true;
    }

    public void Add(Choice choice)
    {
        foreach (Choice c in allChoices)
        {
            if (c == choice)
            {
                visibleChoices.Add(choice);
                UpdateVisualList();
            }
        }
    }

    private List<Choice> ChooseVisiableChoices(List<Choice> choices)
    {
        List<Choice> visiableChoices = new List<Choice>();
        foreach (Choice choice in choices)
        {
            visiableChoices.Add(choice);
        }
        return visiableChoices;
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
        return 3;
    }
}
