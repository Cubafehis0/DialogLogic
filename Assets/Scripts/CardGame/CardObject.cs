using UnityEngine;
using UnityEngine.UI;

public class CardObject : CardObjectBase<Card>
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
        if (titleText) titleText.text = card.Title;
        if (cdtDescText) cdtDescText.text = card.ConditionDesc;
        if (eftDescText) eftDescText.text = card.EffectDesc;
        if (memeText) memeText.text = card.Meme;
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }
}
