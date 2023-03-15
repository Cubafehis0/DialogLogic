using Ink2Unity;
using ModdingAPI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IChoiceSlotReciver
{
    void ChoiceSlotReciver(object msg);
}

public class ChooseGUISystem : MonoBehaviour
{
    [SerializeField]
    private ChooseController controller;
    [SerializeField]
    private List<ChoiceSlotObject> chooseButtons = new List<ChoiceSlotObject>();
    [SerializeField]
    private Button lastButton = null;
    [SerializeField]
    private Button nextButton = null;
    [SerializeField]
    private UnityEvent<ChoiceSlot> onChoose = new UnityEvent<ChoiceSlot>();
    [SerializeField]
    private int pageIndex = 0;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        pageIndex = 0;
        UpdateVisuals();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }



    public void UpdateVisuals()
    {
        if (chooseButtons.Count == 0) return;
        int page = pageIndex / chooseButtons.Count;
        int totalPage = Mathf.CeilToInt(1f * controller.choices.Count / chooseButtons.Count);
        if (lastButton) lastButton.interactable = page > 0;
        if (nextButton) nextButton.interactable = page + 1 < totalPage;
        for (int i = 0; i < chooseButtons.Count; i++)
        {
            if (pageIndex + i < controller.choices.Count)
            {
                chooseButtons[i].gameObject.SetActive(true);
                chooseButtons[i].ChoiceSlot = controller.choices[pageIndex + i];
                chooseButtons[i].UpdateVisuals();
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

    public void SelectChoice()
    {
        GameObject o = EventSystem.current.currentSelectedGameObject;
        if (o == null) return;
        ChoiceSlotObject c = o.GetComponentInParent<ChoiceSlotObject>();
        if (c == null) return;
        Debug.Log("Select Choice " + c.ChoiceSlot.Choice.Content);
        onChoose.Invoke(c.ChoiceSlot);
        SendMessageUpwards(nameof(IChoiceSlotReciver.ChoiceSlotReciver), c.ChoiceSlot, SendMessageOptions.RequireReceiver);
    }


}
