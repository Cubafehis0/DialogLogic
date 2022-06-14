using UnityEngine;
using UnityEngine.UI;

public class RelicObj : MonoBehaviour
{
    public Text nameText;
    public Text descText;
    public Text rarityText;
    public Text categoryText;

    public Relic relic = null;

    public void SetRelic(Relic relic)
    {
        this.relic = relic;
        nameText.text = relic.relicInfo.Name;
        descText.text = relic.relicInfo.Description;
        rarityText.text = Relic.GetRarityString(relic.relicInfo.Rarity);
        categoryText.text = Relic.GetCategoryString(relic.relicInfo.Category);
    }
}