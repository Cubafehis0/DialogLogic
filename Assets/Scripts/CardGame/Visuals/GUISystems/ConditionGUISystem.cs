using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConditionGUISystem : ForegoundGUISystem
{
    public int? nerfNum = null;

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is int nerfNum)) return;
        this.nerfNum = nerfNum;
    }

    public override void Close()
    {
        base.Close();
        nerfNum = null;
    }

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var o = EventSystem.current.currentSelectedGameObject;
            if (o == null) return;
            ChoiceConditionObject c = o.GetComponent<ChoiceConditionObject>();
            if (c == null) return;
            ChoiceSlotObject p = c.GetComponentInParent<ChoiceSlotObject>();
            if (p == null) return;
            Personality modifier = new Personality();
            modifier[c.Type] = nerfNum.Value;
            p.ChoiceSlot.Choice.JudgeValue += modifier;
            Close();
        }
    }
}