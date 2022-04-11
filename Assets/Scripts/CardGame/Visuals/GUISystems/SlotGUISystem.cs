using SemanticTree;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotGUIContext
{
    public EffectList actions;
    public SlotGUIContext(EffectList actions)
    {
        this.actions = actions;
    }
}

public class SlotGUISystem : ForegoundGUISystem,IChoiceSlotReciver
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
        Context.choiceSlotStack.Push(choiceSlot);
        context.actions?.Execute();
        Context.choiceSlotStack.Pop();
        Close();
    }
}