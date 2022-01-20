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
    /// 牌库顶取num张牌
    /// </summary>
    /// <param name="num"></param>
    void Draw(uint num);

    /// <summary>
    /// 抽牌直到手牌满
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
    /// 丢弃所有
    /// </summary>
    void DiscardAll();

    /// <summary>
    /// 弃牌堆洗入抽牌堆
    /// </summary>
    void Discard2Draw();
    void PlayCard(Card card, GameObject target = null);
    void SetupTestPlayer();
    void UpdateObjectReference();
}

public class CardPlayerState : MonoBehaviour, ICardPlayerState, ICardGameListener, Ink2Unity.IPlayerStateChange
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
    /// 起始手牌数
    /// </summary>
    private const uint PLAYER_START_CARD_NUM = 5;

    /// <summary>
    /// 最大手牌数
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
                //手牌满了
                visuals.OnDrawButHandFull();
                return;
            }
            if (drawPile.CardsList.Count == 0)
            {
                //抽牌堆为空
                if (discardPile.CardsList.Count == 0)
                {
                    //没有牌可抽
                    visuals.OnDrawButEmpty();
                    return;
                }
                //洗牌
                Discard2Draw();
            }
            //抽一张
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
    /// 出牌
    /// </summary>
    /// <param name="cardID">出牌id</param>
    /// <returns>是否成功出牌</returns>
    public void PlayCard(Card card, GameObject target)
    {
        if (target == null)
        {
            //瞄准被取消了
            Debug.Log("瞄准被取消了");
        }
        else if (Energy == 0)
        {
            //能量不足
            Debug.Log("能量不足");
            NoTargetAimer.instance.CancelAiming();
        }
        else if (EffectManager.Instance.CheckCanPlay(card))
        {
            //目标有效且可以使用
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
        Debug.Log("我的回合，抽卡！！！");
        Energy = 4;
        Draw(PLAYER_START_CARD_NUM);
    }

    public void OnEndTurn()
    {
        Debug.Log("回合结束");
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
}
