using JasperMod.SemanticTree;
using ModdingAPI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SlotGUIContext
{
    public Action actions;
    public SlotGUIContext(Action actions)
    {
        this.actions = actions;
    }
}

public class SlotGUISystem : ForegoundGUISystem, IChoiceSlotReciver
{
    private SlotGUIContext context;

    [SerializeField]
    private Text title;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void Open(object msg)
    {
        base.Open(msg);
        GUISystemManager.Instance.BorrowSlots(transform);
        gameObject.SetActive(true);
        if (!(msg is SlotGUIContext context)) return;
        this.context = context;
        title.text = "选择一个选项";
    }

    public override void Close()
    {
        base.Close();
        GUISystemManager.Instance.ReturnSlots();
        gameObject.SetActive(false);
    }

    public void ChoiceSlotReciver(object msg)
    {
        ChoiceSlot choiceSlot = (ChoiceSlot)msg;
        Context.choiceSlotStack.Push("choiceSlot");
        context.actions?.Invoke();
        Context.choiceSlotStack.Pop();
        Close();
    }
}