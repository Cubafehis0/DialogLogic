using SemanticTree;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotGUIContext
{
    public EffectList actions;
    public SlotGUIContext(EffectList actions)
    {
        this.actions = actions;
    }
}

public class SlotGUISystem : ForegoundGUISystem
{
    private SlotGUIContext context;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void Open(object msg)
    {
        base.Open(msg);
        gameObject.SetActive(true);
        if (!(msg is SlotGUIContext context)) return;
        this.context = context;
    }

    public override void Close()
    {
        base.Close();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var o = EventSystem.current.currentSelectedGameObject;
            if (o == null) return;
            ChoiceSlot c = o.GetComponent<ChoiceSlot>();
            if (c == null) return;
            Context.choiceSlotStack.Push(c);
            context.actions?.Execute();
            Context.choiceSlotStack.Pop();
        }
    }
}