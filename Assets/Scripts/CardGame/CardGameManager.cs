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
    [SerializeField]
    private UnityEvent OnGameStart;


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
    /// �����֡�
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="action"></param>
    public void OpenCardChoosePanel(List<Card> cards, int num,IEffect action)
    {
        //�������ܸĳ�CardInfo
        throw new System.NotImplementedException();
        //Debug.Log("�򿪿���ѡ�����");
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
        Debug.Log("���ÿ��Ʋ��� δʵ��");
    }

    public void OpenHandChoosePanel(ICondition condition,int num,IEffect action)
    {
        WaitGUI = true;
        HandSelectSystem.Instance.Open(CardPlayerState.Instance.Hand, num, action);
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
        //������GUI���治ͳһ
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
            if (CardPlayerState.Instance.CanChoose(slot))
            {
                DialogSystem.Instance.ForceSelectChoice(slot.Choice, CardPlayerState.Instance.JudgeChooseSuccess(slot));
            }
        }
    }

    /// <summary>
    /// ����һ����Ϸ
    /// </summary>
    public void StartGame()
    {
        turn = 0;
        OnGameStart.Invoke();
    }
    /// <summary>
    /// ������ǰ�غ�
    /// </summary>
    public void EndTurn()
    {
        CardPlayerState.Instance.EndTurn();
    }

    /// <summary>
    /// ����һ���غ�
    /// </summary>
    public void StartTurn()
    {
        turn++;
        CardPlayerState.Instance.StartTurn();
    }
}
