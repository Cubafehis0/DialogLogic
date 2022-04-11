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

public class SlotGUISystem : ForegoundGUISystem
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
        GUISystemManager.Instance.chooseSystem.enabled = false;
        gameObject.SetActive(true);
        if (!(msg is SlotGUIContext context)) return;
        this.context = context;
        title.text = "选择一个选项";
    }

    public override void Close()
    {
        base.Close();
        GUISystemManager.Instance.chooseSystem.DelayActivate();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject o = EventSystem.current.currentSelectedGameObject;
            if (o == null) return;
            ChoiceSlotObject c = o.GetComponentInParent<ChoiceSlotObject>();
            if (c == null) return;
            Context.choiceSlotStack.Push(c.ChoiceSlot);
            context.actions?.Execute();
            Context.choiceSlotStack.Pop();
            Close();
        }
    }
}