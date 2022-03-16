using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface ICardPlayerState
{
    ICharacter Character { get; }
    IPlayer Player { get; }
    int Energy { get; set; }
    IPile Hand { get; }
    IPile DrawPile { get; }
    IPile DiscardPile { get; }
    CardLibrary DynamicCardLibrary { get; }

    /// <summary>
    /// �ƿⶥȡnum����
    /// </summary>
    /// <param name="num"></param>
    void Draw(uint num);

    /// <summary>
    /// ����ֱ��������
    /// </summary>
    void Draw2Full();

    /// <summary>
    /// 
    /// </summary>
    void Init();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cid"></param>
    void DiscardCard(uint cid);

    /// <summary>
    /// ��������
    /// </summary>
    void DiscardAll();

    /// <summary>
    /// ���ƶ�ϴ����ƶ�
    /// </summary>
    void Discard2Draw();
    void PlayCard(Card card, GameObject target = null);
    void SetupTestPlayer();
    void UpdateObjectReference();
}

public class CardPlayerState : MonoBehaviour, ICardPlayerState, ICardGameListener, Ink2Unity.IPlayerStateChange,ICardOperationSubject
{
    private static CardPlayerState instance = null;
    public static CardPlayerState Instance { get => instance; }


    [SerializeField]
    private Character character = null;
    public ICharacter Character { get => character; }

    [SerializeField]
    private Player player = null;
    public IPlayer Player { get => player; }

    [SerializeField]
    private int energy;
    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            OnEnergyChange.Invoke();
        }
    }

    [SerializeField]
    private Pile hand = null;
    public IPile Hand { get => hand; }

    [SerializeField]
    private Pile drawPile = null;
    public IPile DrawPile { get => drawPile; }

    [SerializeField]
    private Pile discardPile = null;
    public IPile DiscardPile { get => discardPile; }

    
    private CardLibrary dynamicCardLibrary = null;
    public CardLibrary DynamicCardLibrary { get => dynamicCardLibrary; }
   
    private ICardPlayerStateObject visuals;

    public UnityEvent OnEnergyChange = new UnityEvent();

    /// <summary>
    /// ��ʼ������
    /// </summary>
    private const uint PLAYER_START_CARD_NUM = 5;

    /// <summary>
    /// ���������
    /// </summary>
    private const uint PLAYER_HAND_CARD_MAXNUM = 10;

    private void Awake()
    {
        instance = this;
        dynamicCardLibrary = GetComponent<CardLibrary>();
        if (dynamicCardLibrary == null)
        {
            var lib = gameObject.AddComponent<CardLibrary>();
            dynamicCardLibrary = lib;
        }
        //if (character == null)
        //{
        //    character = gameObject.AddComponent(typeof(Character)) as Character;
        //}
    }

    private void Start()
    {
        BaseAimer nta = NoTargetAimer.instance;
        if (nta) nta.AddCallback(PlayCard);
        BaseAimer ota = OneTargetAimer.instance;
        if (ota) ota.AddCallback(PlayCard);
    }

    public void Draw(uint num)
    {
        for (int i = 0; i < num; i++)
        {
            if (hand.CardsList.Count == PLAYER_HAND_CARD_MAXNUM)
            {
                //��������
                visuals.OnDrawButHandFull();
                return;
            }
            if (drawPile.CardsList.Count == 0)
            {
                //���ƶ�Ϊ��
                if (discardPile.CardsList.Count == 0)
                {
                    //û���ƿɳ�
                    visuals.OnDrawButEmpty();
                    return;
                }
                //ϴ��
                Discard2Draw();
            }
            //��һ��
            PileMigrateUtils.MigrateTo(drawPile.GetCardByOrderID(0), hand);
            visuals.OnDraw();
        }
    }

    public void Draw2Full()
    {
        throw new System.NotImplementedException();
    }

    public void DiscardCard(Card cid)
    {
        PileMigrateUtils.MigrateTo(cid.GetComponent<Card>(), discardPile);
    }

    public void DiscardAll()
    {
        hand.MigrateAllTo(discardPile);
    }

    public void SetupTestPlayer()
    {
        int[] data = { 0, 1, 2, 3, 4, 5, 6 };
        player.CardSet.AddRange(data);
    }

    public void Init()
    {
        foreach (int i in player.CardSet)
        {
            Card newCard = StaticCardLibrary.Instance.GetCardCopyByID(i);
            drawPile.Add(newCard);
        }
        drawPile.Shuffle();
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="cardID">����id</param>
    /// <returns>�Ƿ�ɹ�����</returns>
    public void PlayCard(Card card, GameObject target)
    {
        if (target == null)
        {
            //��׼��ȡ����
            Debug.Log("��׼��ȡ����");
        }
        else if (Energy == 0)
        {
            //��������
            Debug.Log("��������");
            NoTargetAimer.instance.CancelAiming();
        }
        else if (EffectManager.Instance.CheckCanPlay(card))
        {
            //Ŀ����Ч�ҿ���ʹ��
            Energy--;
            if (EffectManager.Instance.Play(card))
            {
                PileMigrateUtils.MigrateTo(card, discardPile);
            }
        }
    }

    public void UpdateObjectReference()
    {
        visuals = GetComponent<ICardPlayerStateObject>();
    }

    public void Discard2Draw()
    {
        discardPile.MigrateAllTo(drawPile);
        drawPile.Shuffle();
    }

    public void DiscardCard(uint cid)
    {
        throw new System.NotImplementedException();
    }

    public void OnStartTurn()
    {
        Debug.Log("�ҵĻغϣ��鿨������");
        Energy = 4;
        Draw(PLAYER_START_CARD_NUM);
    }

    public void OnEndTurn()
    {
        Debug.Log("�غϽ���");
        DiscardAll();
    }

    public void OnStartGame()
    {
        SetupTestPlayer();
        Init();
    }

    public void StateChange(List<int> delta, int turn)
    {
        Debug.Log("StateChange");
    }

    public void PressureChange(int delta)
    {
        Debug.Log("PressureChange");
    }
    public UnityEvent<Card, uint> OnDrawCard { get; set; }
    public UnityEvent<Card, uint> OnPlayCard { get; set; }
    public UnityEvent<Card, uint> OnDiscardCard { get; set; }
}
