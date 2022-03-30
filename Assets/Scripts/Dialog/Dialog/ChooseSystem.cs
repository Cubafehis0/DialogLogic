using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseSystem : MonoBehaviour
{
    [SerializeField]
    private List<RichButton> chooseButtons = new List<RichButton>();
    [SerializeField]
    private Button lastButton = null;
    [SerializeField]
    private Button nextButton = null;
    [SerializeField]
    private Sprite[] spritesOfButtonA = null;

    private int pageIndex = 0;
    private List<ChoiceSlot> allChoices = new List<ChoiceSlot>();
    

    private static ChooseSystem instance = null;
    public static ChooseSystem Instance { get => instance; }

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
        if (lastButton) lastButton.onClick.AddListener(PreviousPage);
        if (nextButton) nextButton.onClick.AddListener(NextPage);
        foreach (var btn in chooseButtons)
        {
            btn.OnClick.AddListener(ClickChoiceButton);
        }
        gameObject.SetActive(false);
    }

    public void Open(List<Choice> choices)
    {
        if (choices == null) return;
        pageIndex = 0;
        allChoices.Clear();
        foreach(Choice choice in choices)
        {
            allChoices.Add(new ChoiceSlot { Choice = choice });
        }
        gameObject.SetActive(true);
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (chooseButtons.Count == 0) return;
        int page = pageIndex / chooseButtons.Count;
        int totalPage = Mathf.CeilToInt(1f * allChoices.Count / chooseButtons.Count);
        if (lastButton) lastButton.interactable = page > 0 ;
        if (nextButton) nextButton.interactable = page + 1 < totalPage;
        for (int i = 0; i < chooseButtons.Count; i++)
        {
            if (pageIndex + i < allChoices.Count)
            {
                chooseButtons[i].gameObject.SetActive(true);
                chooseButtons[i].SetText(allChoices[pageIndex + i].Choice.Content.richText);
                chooseButtons[i].SetSprite(spritesOfButtonA[ChooseSprite(allChoices[pageIndex + i].Choice.BgColor)]);
                if (allChoices[pageIndex + i].Locked)
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

    public void PreviousPage()
    {
        pageIndex -= chooseButtons.Count;
        UpdateVisuals();
    }

    public void NextPage()
    {
        pageIndex += chooseButtons.Count;
        UpdateVisuals();
    }

    private void ClickChoiceButton(RichButton from)
    {
        SelectChoice(chooseButtons.IndexOf(from) + pageIndex);
    }

    public bool SelectChoice(int choiceIndex)
    {
        CardPlayerState.Instance.SelectChoice(allChoices[choiceIndex]);
        return true;
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
