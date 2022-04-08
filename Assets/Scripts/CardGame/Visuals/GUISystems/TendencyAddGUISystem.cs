using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TendencyAddGUIContext
{
    public HashSet<PersonalityType> mask;
    public int num;

    public TendencyAddGUIContext(HashSet<PersonalityType> mask, int num)
    {
        this.mask = mask;
        this.num = num;
    }
}
public class TendencyAddGUISystem : ForegoundGUISystem
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

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public override void Open(object msg)
    {
        base.Open(msg);
        gameObject.SetActive(true);
        if (!(msg is TendencyAddGUIContext context)) return;
        if (context.num <= 0) return;
        this.num = context.num;
        insideButton.interactable = context.mask.Contains(PersonalityType.Inside);
        outsideButton.interactable = context.mask.Contains(PersonalityType.Outside);
        logicButton.interactable = context.mask.Contains(PersonalityType.Logic);
        passionButton.interactable = context.mask.Contains(PersonalityType.Passion);
        moralButton.interactable = context.mask.Contains(PersonalityType.Moral);
        unethicButton.interactable = context.mask.Contains(PersonalityType.Unethic);
        detourButton.interactable = context.mask.Contains(PersonalityType.Detour);
        strongButton.interactable = context.mask.Contains(PersonalityType.Strong);
    }

    public override void Close()
    {
        base.Close();
        gameObject.SetActive(false);
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
                CardGameManager.Instance.player.Player.PlayerInfo.Personality += modifier;
                num--;
                break;
            default:
                break;
        }
        if (num == 0)
        {
            gameObject.SetActive(false);
            Close();
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
