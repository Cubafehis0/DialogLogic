using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITable
{
    void Open();
    void Close();
}

public class TendencyTable : MonoBehaviour, ITable
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
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

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

    public void SelectTendency(Button button)
    {

    }
}
