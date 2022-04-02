using System.Collections;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[ExecuteAlways]
public class ChoiceConditionObject : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private PersonalityType type;
    [SerializeField]
    private int value;
    [SerializeField]
    private bool reveal;

    public PersonalityType Type { get => type; set => type = value; }
    public int Value { get => value; set => this.value = value; }
    public bool Reveal { get => reveal; set => reveal = value; }

    
    private void Awake()
    {
        image=GetComponent<Image>();
    }

    private void Update()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (reveal) image.sprite = GameManager.Instance.ConditionIcons[type];
        else image.sprite = GameManager.Instance.ConditonCover;
    }

    public void OpenHUD()
    {
        if (reveal)
        {
            var msg = string.Format("判定:{0}", value);
            HoverHUDSystem.Instance.OpenConditionHUD(msg);
        }

    }

    public void CloseHUD()
    {
        HoverHUDSystem.Instance.CloseConditionHUD();
    }


}