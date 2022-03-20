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

    private static CardGameManager instance = null;
    public UnityEvent OnStartGame = new UnityEvent();

    public static CardGameManager Instance
    {
        get => instance;
    }
    public Card EmptyCard
    {
        
        get
        {
            return Instantiate(emptyCard);
        }
    }

    public CardObject GetCardObject(Card card)
    {
        GameObject gameObject = Instantiate(card.gameObject);
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.rotation = Quaternion.identity;
        return gameObject.GetComponent<CardObject>();
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
    /// ѡ�������ֻѡһ��
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="action"></param>
    public void OpenCardChoosePanel(List<Card> cards, int num,IEffectNode action)
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

    public void OpenHandChoosePanel(IConditionNode condition,int num,IEffectNode action)
    {
        HandSelectSystem.Instance.Open(CardPlayerState.Instance.Hand, num, action);
    }

    public void OpenPileChoosePanel(List<Card> cards, int num, IEffectNode action)
    {
        PileSelectSystem.Instance.Open(cards, num, action);
    }

    public void OpenSlotSelectPanel(IChoiceSlotEffectNode action)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ����һ����Ϸ
    /// </summary>
    public void StartGame()
    {
        OnStartGame.Invoke();
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
        CardPlayerState.Instance.StartTurn();
    }
}
