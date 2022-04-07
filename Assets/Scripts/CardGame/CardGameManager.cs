using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;


public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private int turn = 0;
    [SerializeField]
    private TendencyTable tendencyTable;
    [SerializeField]
    private ChooseSystem chooseSystem;

    public CardPlayerState player;
    public CardPlayerState enemy;

    public bool WaitGUI;
    private EffectList effectList;

    private static CardGameManager instance = null;
    public static CardGameManager Instance
    {
        get => instance;
    }

    public ChooseSystem ChooseSystem { get { return chooseSystem; } }
    public int Turn { get => turn;}

    public Card GetCardCopy(Card prefab)
    {
        Card ret=Instantiate(prefab.gameObject).GetComponent<Card>();
        ret.Construct(prefab);
        return ret;
    }

    public void ReturnCardObject(CardObject cardObject)
    {
        Destroy(cardObject.gameObject);
    }

    void Awake()
    {
        instance = this;
        ExpressionAnalyser.ExpressionParser.VariableTable = new Context();
        tendencyTable.gameObject.SetActive(false);
    }

    public void OpenTendencyChoosePanel(HashSet<PersonalityType> types,int value)
    {
        WaitGUI = true;
        tendencyTable.Open(types, value);
    }

    /// <summary>
    /// 【发现】
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="action"></param>
    public void OpenCardChoosePanel(List<Card> cards, int num,IEffect action)
    {
        //参数不能改成CardInfo
        throw new System.NotImplementedException();
        //Debug.Log("打开卡牌选择面板");
        //List<Card> cardObjects = new List<Card>();
        //foreach(Card card in cards)
        //{
        //    Card newCard = Instantiate(card, transform);
        //    newCard.gameObject.SetActive(true);
        //    cardObjects.Add(newCard);
        //}
    }

    public void DisableInput()
    {
        WaitGUI = true;
    }

    public void EnableInput()
    {
        Debug.Log("启用卡牌操作 未实现");
    }

    public void OpenHandChoosePanel(ICondition condition,int num,IEffect action)
    {
        WaitGUI = true;
        HandSelectSystem.Instance.Open(player.Hand, num, action);
    }

    public void OpenPileChoosePanel(List<Card> cards, int num, IEffect action)
    {
        WaitGUI = true;
        PileSelectSystem.Instance.Open(cards, num, action);
    }

    public void OpenPileChoosePanel(List<Card> cards, int num, EffectList action)
    {
        WaitGUI = true;
        PileSelectSystem.Instance.Open(cards, num, action);
    }

    public void OpenSlotSelectPanel(EffectList action)
    {
        //与其他GUI界面不统一
        WaitGUI = true;
        effectList = action;
    }

    public void OpenConditionNerfPanel(int value)
    {
        throw new System.NotImplementedException();
    }

    public void SlotSelectCallback(ChoiceSlot slot)
    {
        if (WaitGUI)
        {
            Context.choiceSlotStack.Push(slot);
            effectList?.Execute();
            Context.choiceSlotStack.Pop();
            effectList = null;
            WaitGUI = false;
        }
        else
        {
            if (player.CanChoose(slot))
            {
                DialogSystem.Instance.ForceSelectChoice(slot.Choice, player.JudgeChooseSuccess(slot));
            }
        }
    }

    /// <summary>
    /// 开启一局游戏
    /// </summary>
    public void StartGame()
    {
        turn = 0;
    }
    /// <summary>
    /// 结束当前回合
    /// </summary>
    public void EndTurn()
    {
        player.EndTurn();
    }

    /// <summary>
    /// 开启一个回合
    /// </summary>
    public void StartTurn()
    {
        turn++;
        player.StartTurn();
    }
}
