using UnityEngine;
using UnityEngine.UI;

public abstract class VisualObject<T> : MonoBehaviour
{
    public T target;

    public abstract void UpdateVisuals();
}

public class RelicObj : VisualObject<Relic>
{
    public Text nameText;
    public Text descText;
    public Text rarityText;
    public Text categoryText;

    private void Update()
    {
        UpdateVisuals();
    }

    public override void UpdateVisuals()
    {
        if (target != null)
        {
            nameText.text = target.Name;
            descText.text = target.Description;
            rarityText.text = Localizer.GetRarityString(target.Rarity);
            categoryText.text = Localizer.GetCategoryString(target.Category);
        }
    }
}