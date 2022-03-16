using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree.ChoiceSemantics;
using SemanticTree;
public enum CardGameTriggerType
{
    OnTurnStart,
    OnTurnEnd,
    OnPlayCard
}


public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private Card emptyCard = null;
    [SerializeField]
    private GameObject HighlightCanvs = null;

    private static CardGameManager instance = null;
    public UnityEvent OnStartGame = new UnityEvent();

    public static CardGameManager Instance
    {
        get => instance;
    }
    public Card EmptyCard
    {
        get => Instantiate(emptyCard);
    }
    void Awake()
    {
        instance = this;
    }

    public void OpenTendencyChoosePanel(int mask)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 选择很少且只选一张
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="action"></param>
    public void OpenCardChoosePanel(List<Card> cards, int num,IEffectNode action)
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

    public void OpenHandChoosePanel(IConditionNode condition,int num,IEffectNode action)
    {
        HighlightCanvs.SetActive(true);
        HandSelectSystem.Instance.Open(CardPlayerState.Instance.Hand, num, action);
        //禁用输入
    }

    public void OpenPileChoosePanel(List<Card> cards, int num, IEffectNode action)
    {
        throw new System.NotImplementedException();
    }

    public void OpenSlotSelectPanel(IChoiceSlotEffectNode action)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 开启一局游戏
    /// </summary>
    public void StartGame()
    {
        OnStartGame.Invoke();
    }
    /// <summary>
    /// 结束当前回合
    /// </summary>
    public void EndTurn()
    {
        CardPlayerState.Instance.EndTurn();
    }

    /// <summary>
    /// 开启一个回合
    /// </summary>
    public void StartTurn()
    {
        CardPlayerState.Instance.StartTurn();
    }
}
