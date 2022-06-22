using UnityEngine;
using UnityEngine.UI;

public class CardObject : CardObjectBase
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text cdtDescText;
    [SerializeField]
    private Text eftDescText;
    [SerializeField]
    private Text memeText;

    public override void UpdateVisuals()
    {
        if (card == null) return;
        
        //if (titleText) titleText.text = card.info.Title;
        //if (cdtDescText) cdtDescText.text = card.info.ConditionDesc;
        //if (eftDescText) eftDescText.text = card.info.EffectDesc;
        //if (memeText) memeText.text = card.info.Meme;
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }
}
