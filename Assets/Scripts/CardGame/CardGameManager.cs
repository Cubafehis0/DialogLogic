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

    public void DisableInput()
    {
        Debug.Log("���ÿ��Ʋ��� δʵ��");
    }

    public void EnableInput()
    {
        Debug.Log("���ÿ��Ʋ��� δʵ��");
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
        Debug.LogWarning("Ч��δ���");
        PileSelectSystem.Instance.Open(cards, num, action[0]);
    }

    public void OpenSlotSelectPanel(EffectList action)
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
