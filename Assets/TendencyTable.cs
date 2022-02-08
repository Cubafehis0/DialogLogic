using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITable
{
    void Open();
    void Close();
}
public interface ITendencyTable : ITable
{
    void Init(TendencyTable.Mask mask);
}
public class TendencyTable : MonoBehaviour, ITendencyTable
{

    private static ITendencyTable instance = null;
    public static ITendencyTable Instance { get => instance; }
    public enum Mask
    {
        Detour = 1,
        Strong = 2,
        Moral = 4,
        Unethic = 8,
        Logic = 16,
        Passion = 32,
        Inside = 64,
        Outside = 128,
        All = 255
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
        Init((Mask)11);
    }

    public void Init(Mask mask)
    {
        detourButton.gameObject.SetActive(((int)mask & (int)Mask.Detour) > 0);
        strongButton.gameObject.SetActive(((int)mask & (int)Mask.Strong) > 0);
        moralButton.gameObject.SetActive(((int)mask & (int)Mask.Moral) > 0);
        unethicButton.gameObject.SetActive(((int)mask & (int)Mask.Unethic) > 0);
        logicButton.gameObject.SetActive(((int)mask & (int)Mask.Logic) > 0);
        passionButton.gameObject.SetActive(((int)mask & (int)Mask.Passion) > 0);
        insideButton.gameObject.SetActive(((int)mask & (int)Mask.Inside) > 0);
        outsideButton.gameObject.SetActive(((int)mask & (int)Mask.Outside) > 0);
    }

    //public void SelectTendency(Button button)
    //{

    //}

    private void OnClickButton(Mask mask)
    {
        //关闭面板
        Close();

        //传递消息，待实现
        Debug.LogError("传递消息，待实现，选择了" + mask.ToString());
    }

    private void Start()
    {
        Close();
        instance = this;
        detourButton.onClick.AddListener(delegate { OnClickButton(Mask.Detour); });
        strongButton.onClick.AddListener(delegate { OnClickButton(Mask.Strong); });
        moralButton.onClick.AddListener(delegate { OnClickButton(Mask.Moral); });
        unethicButton.onClick.AddListener(delegate { OnClickButton(Mask.Unethic); });
        logicButton.onClick.AddListener(delegate { OnClickButton(Mask.Logic); });
        passionButton.onClick.AddListener(delegate { OnClickButton(Mask.Passion); });
        insideButton.onClick.AddListener(delegate { OnClickButton(Mask.Inside); });
        outsideButton.onClick.AddListener(delegate { OnClickButton(Mask.Outside); });     
    }
}
