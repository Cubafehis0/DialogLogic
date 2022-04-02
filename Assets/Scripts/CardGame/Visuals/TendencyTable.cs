using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TendencyTable : MonoBehaviour
{
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
    [SerializeField]
    private int num;
    public void Open(HashSet<PersonalityType> mask,int num)
    {
        Debug.Assert(gameObject.activeSelf == false,"加点窗口已经打开");
        if (num <= 0) return;
        this.num = num;
        insideButton.interactable = mask.Contains(PersonalityType.Inside);
        outsideButton.interactable = mask.Contains(PersonalityType.Outside);
        logicButton.interactable = mask.Contains(PersonalityType.Logic);
        passionButton.interactable = mask.Contains(PersonalityType.Passion);
        moralButton.interactable = mask.Contains(PersonalityType.Moral);
        unethicButton.interactable = mask.Contains(PersonalityType.Unethic);
        detourButton.interactable = mask.Contains(PersonalityType.Detour);
        strongButton.interactable = mask.Contains(PersonalityType.Strong);
        gameObject.SetActive(true); 
    }

    public void SelectTendency()
    {
        var addon = CurrentSelectedTendency();
        switch (addon)
        {
            case PersonalityType.Inside:
            case PersonalityType.Outside:
            case PersonalityType.Logic:
            case PersonalityType.Passion:
            case PersonalityType.Moral:
            case PersonalityType.Unethic:
            case PersonalityType.Detour:
            case PersonalityType.Strong:
                Personality modifier = new Personality();
                modifier[addon.Value] = 1;
                CardPlayerState.Instance.Player.PlayerInfo.Personality += modifier;
                num--;
                break;
            default:
                break;
        }
        if (num == 0)
        {
            gameObject.SetActive(false);
            CardGameManager.Instance.WaitGUI = false;
        }
    }

    private PersonalityType? CurrentSelectedTendency()
    {
        GameObject currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        Button currentSelectedBtn = currentSelectedObject.GetComponent<Button>();
        if (currentSelectedBtn == insideButton) return PersonalityType.Inside;
        if (currentSelectedBtn == outsideButton) return PersonalityType.Outside;
        if (currentSelectedBtn == unethicButton) return PersonalityType.Unethic;
        if (currentSelectedBtn == passionButton) return PersonalityType.Passion;
        if (currentSelectedBtn == logicButton) return PersonalityType.Logic;
        if (currentSelectedBtn == moralButton) return PersonalityType.Moral;
        if (currentSelectedBtn == detourButton) return PersonalityType.Detour;
        if (currentSelectedBtn == strongButton) return PersonalityType.Strong;
        return null;
    }
}
