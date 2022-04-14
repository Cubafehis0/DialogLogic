using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConditionGUISystem : ForegoundGUISystem
{
    [SerializeField]
    private int nerfNum = 0;
    [SerializeField]
    private Text title;

    public override void Open(object msg)
    {
        base.Open(msg);
        if (!(msg is int nerfNum)) return;
        this.nerfNum = nerfNum;
        title.text = string.Format("选择1个已揭示条件判定-{0}", nerfNum);
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
            modifier[c.Type] = -nerfNum;
            p.ChoiceSlot.Choice.JudgeValue += modifier;
            Close();
        }
    }
}