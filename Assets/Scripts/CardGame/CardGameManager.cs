using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;
using XmlParser;
using SemanticTree.ChoiceEffect;

public enum CardGameTriggerType
{
    OnTurnStart,
    OnTurnEnd,
    OnPlayCard
}


public class CardGameManager : MonoBehaviour
{
    private static CardGameManager instance = null;
    public UnityEvent OnStartGame = new UnityEvent();

    public static CardGameManager Instance
    {
        get => instance;
    }

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

    public void DisableInput()
    {
        Debug.Log("禁用卡牌操作 未实现");
    }

    public void EnableInput()
    {
        Debug.Log("启用卡牌操作 未实现");
    }

    public void OpenHandChoosePanel(ICondition condition,int num,IEffectNode action)
    {
        HandSelectSystem.Instance.Open(CardPlayerState.Instance.Hand, num, action);
    }

    public void OpenPileChoosePanel(List<Card> cards, int num, IEffectNode action)
    {
        PileSelectSystem.Instance.Open(cards, num, action);
    }

    public void OpenPileChoosePanel(List<Card> cards, int num, EffectList action)
    {
        Debug.LogWarning("效果未完成");
        PileSelectSystem.Instance.Open(cards, num, action[0]);
    }

    public void OpenSlotSelectPanel(EffectList action)
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
