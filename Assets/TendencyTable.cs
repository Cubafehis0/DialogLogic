using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TendencyTable : MonoBehaviour
{
    public enum Mask
    {
        Strong = 1,
        Moral = 2,
        Logic = 4,
        Inside = 8,
        All = 15
    }

    [SerializeField]
    private Button detourButton = null;
    [SerializeField]
    private Button strongButton = null;
    [SerializeField]
    private Button moralButton = null;
    [SerializeField]
    private Button unethicButton = null;
    [SerializeField]
    private Button logicButton = null;
    [SerializeField]
    private Button passionButton = null;
    [SerializeField]
    private Button insideButton = null;
    [SerializeField]
    private Button outsideButton = null;
    public void Init(Mask mask)
    {
        detourButton.interactable = ((int)mask & (int)Mask.Strong) > 0;
        strongButton.interactable = ((int)mask & (int)Mask.Strong) > 0;
        moralButton.interactable = ((int)mask & (int)Mask.Moral) > 0;
        unethicButton.interactable = ((int)mask & (int)Mask.Moral) > 0;
        logicButton.interactable = ((int)mask & (int)Mask.Logic) > 0;
        passionButton.interactable = ((int)mask & (int)Mask.Logic) > 0;
        insideButton.interactable = ((int)mask & (int)Mask.Inside) > 0;
        outsideButton.interactable = ((int)mask & (int)Mask.Inside) > 0;
    }

    public void SelectTendency()
    {

    }

    private PersonalityType CurrentSelectedTendency()
    {
        GameObject currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        Button currentSelectedBtn = currentSelectedObject.GetComponent<Button>();
        if (currentSelectedBtn == insideButton) return PersonalityType.Inside;
        if(currentSelectedBtn == outsideButton) return PersonalityType.Outside;
        if(currentSelectedBtn == unethicButton) return PersonalityType.Unethic;
        if(currentSelectedBtn == passionButton) return PersonalityType.Passion;
        return PersonalityType.Inside;
    }
}
