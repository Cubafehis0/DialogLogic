using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityObject : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image image;

    private AbilityCounter counter;

    public void Set(AbilityCounter counter)
    {
        this.counter = counter;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        image.sprite = AbilityLibrary.Instance.spriteDictionary[counter.Ability.icon];
        text.text = counter.Count.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
