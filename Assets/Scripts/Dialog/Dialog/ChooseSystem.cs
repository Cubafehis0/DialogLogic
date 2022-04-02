using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ChooseSystem : MonoBehaviour
{
    [SerializeField]
    private List<ChoiceSlotObject> chooseButtons = new List<ChoiceSlotObject>();
    [SerializeField]
    private Button lastButton = null;
    [SerializeField]
    private Button nextButton = null;
    [SerializeField]
    private List<ChoiceSlot> allChoices = new List<ChoiceSlot>();
    [SerializeField]
    private UnityEvent<ChoiceSlot> onChoose = new UnityEvent<ChoiceSlot>();

    private int pageIndex = 0;

    public IReadOnlyList<ChoiceSlot> Choices { get => allChoices; }

    public UnityEvent<ChoiceSlot> OnChoose { get => onChoose; }
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Open(List<Choice> choices)
    {
        if (choices == null) return;
        pageIndex = 0;
        allChoices.Clear();
        foreach (Choice choice in choices)
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
        if (lastButton) lastButton.interactable = page > 0;
        if (nextButton) nextButton.interactable = page + 1 < totalPage;
        for (int i = 0; i < chooseButtons.Count; i++)
        {
            if (pageIndex + i < allChoices.Count)
            {
                chooseButtons[i].gameObject.SetActive(true);
                chooseButtons[i].ChoiceSlot = allChoices[pageIndex + i];
                chooseButtons[i].UpdateVisuals();
            }
            else
            {
                chooseButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public List<ChoiceSlot> GetChoiceSlot(SpeechType type)
    {
        return allChoices.FindAll(x => x.Choice.SpeechType == type);
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

    public void SelectChoice()
    {
        GameObject o = EventSystem.current.currentSelectedGameObject;
        if (o == null) return;
        ChoiceSlotObject c = o.GetComponentInParent<ChoiceSlotObject>();
        if (c == null) return;
        Debug.Log("Select Choice "+c.ChoiceSlot.Choice.Content);
        if (chooseButtons.Contains(c))
        {
            onChoose.Invoke(c.ChoiceSlot);
        }
    }
}
