using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public interface IPlayerStateChange
{
    //values为四维值的改变值，turns为改变持续回合数，为-1时表示无限回合
    void StateChange(List<int> values,int turns);
    //压力槽改变值，压力槽满时改为扣除value/3取整后的san值
    void PressureChange(int value);
}


public class CardGameManager : MonoBehaviour,IPlayerStateChange
{
    [SerializeField]
    private CardTable cardTable;
    
    private static CardGameManager _instance = null;
    public static CardGameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        ParseCardData();
        InitPlayer();
    }

    
    private Dictionary<uint, Card> cards = new Dictionary<uint, Card>();
    /// <summary>
    /// 玩家状态信息，八维，行动点
    /// </summary>
    public Player player = new Player();
    private List<uint> deck = new List<uint>();
    private List<uint> garbageDeck = new List<uint>();
    private CardGameState gameState = CardGameState.GAME_START;
    public CardGameStatistics statistics = new CardGameStatistics();
    
    
    private const uint PLAYER_START_CARD_NUM = 5;
    private const uint PLAYER_HAND_CARD_NUM = 10;

    /// <summary>
    /// 供effect与游戏流程中的同步使用
    /// </summary>
    private Dictionary<string, List<Signal>> signalDictionary = new Dictionary<string, List<Signal>>(); 
    
    /// <summary>
    /// 设置信息
    /// </summary>
    /// <param name="key">时间点</param>
    /// <param name="sig">信息</param>
    /// <param name="clear">是否清除</param>
    public void SetSignal(string key,Signal sig,bool clear = false)
    {
        List<Signal> signals;
        if (signalDictionary.TryGetValue(key, out signals))
        {
            if (!clear)
            {
                signalDictionary[key].Add(sig);
            }
            else
            {
                signalDictionary.Clear();
            }
        }
        else
        {
            if (!clear)
            {
                signalDictionary.Add(key, new List<Signal>());
            }
        }
    }
    
    /// <summary>
    /// 获取信息 使用时检查null
    /// </summary>
    /// <param name="key">时间点</param>
    /// <param name="updateTurn">是否更新次数</param>
    /// <returns></returns>
    private List<Signal> getSignal(string key,bool updateTurn = true)
    {
        List<Signal> signals;
        if (signalDictionary.TryGetValue(key, out signals))
        {
            foreach (var sig in signals)
            {
                if (updateTurn)
                {
                    sig.lastTurns--;
                    if (sig.lastTurns == 0)
                    {
                        signals.Remove(sig);
                    }
                }
            }
            return signals;
        }
        else
        {
            return null;
        }
    }


    private static void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0,n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }

    private void InitPlayer()
    {
        uint[] data = {1000, 1001, 1002, 1003, 1004, 1005,1006};
        player.cardSet.AddRange(data);
    }
    private void ParseCardData()
    {
        foreach (CardEntity entity in cardTable.Sheet1)
        {
            cards.Add(entity.id,
                new Card(entity.id, entity.hold_effect,entity.hold_effect_scale, entity.condition,entity.condition_scale,entity.effect,entity.effect_scale,entity.post_effect,entity.post_effect_scale));
        }
    }
    
    
    public void Draw(uint num)
    {
        if (deck.Count < num)
        {
            deck.AddRange(garbageDeck);
            Shuffle(deck);
            garbageDeck.Clear();
        }
        while (player.handCard.Count < PLAYER_HAND_CARD_NUM && num > 0 && deck.Count > 0)
        {
            int id = Random.Range(0, deck.Count);
            player.handCard.Add(deck[id]);
            deck.RemoveAt(id);
            num--;
        }
    }

    private void ClearTurnOnlySignals()
    {
        foreach (var signals in signalDictionary.Values)
        {
            foreach (var sig in signals)
            {
                if (!sig.flag)
                {
                    signals.Remove(sig);
                }
            }
        }
    }
    private void DiscardAll()
    {
        garbageDeck.AddRange(player.handCard);
        player.handCard.Clear();
    }
    /// <summary>
    /// 开启一局游戏
    /// </summary>
    public void StartGame()
    {
        gameState = CardGameState.GAME_START;
        NextGameState();
    }
    /// <summary>
    /// 结束本局游戏
    /// </summary>
    public void EndGame()
    {
        ClearData();
        gameState = CardGameState.GAME_END;
    }
    /// <summary>
    /// 结束当前回合
    /// </summary>
    public void EndTurn()
    {
        gameState = CardGameState.TURN_END;
        NextGameState();
    }
    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="cardID">出牌id</param>
    /// <returns>是否能够出牌</returns>
    public bool PlayCard(uint cardID)
    {
        if (gameState != CardGameState.PLAYER_PLAY)
        {
            Debug.LogError("当前不是出牌回合！");
            return false;
        }

        bool res = cards[cardID].CheckCanPlay();
        res &= player.energy > 0;
        if (res)
        {
            player.energy--;
            NextGameState(cardID);
        }
        return res;
    }
    /// <summary>
    /// 开启一个回合
    /// </summary>
    public void StartTurn()
    {
        gameState = CardGameState.TURN_START;
        NextGameState();
    }

    private void ClearData()
    {
        deck.Clear();
        garbageDeck.Clear();
        signalDictionary.Clear();
        statistics.Clear();
    }
    private void NextGameState(uint cardID = 0)
    {
        switch (gameState)
        {
            case CardGameState.GAME_START:
                Debug.Log("GAME START!");
                ClearData();
                deck.AddRange(player.cardSet);
                Shuffle(deck);
                player.handCard.Clear();
                gameState = CardGameState.TURN_START;
                break;
            case CardGameState.TURN_START:
                Debug.Log("TURN START!");
                player.originData.CopyTo(player.data,0);
                Draw(PLAYER_START_CARD_NUM);
                Debug.Log("MY TURN, DRAW!");
                gameState = CardGameState.AUTO_PLAY;
                NextGameState();
                break;
            case CardGameState.AUTO_PLAY:
                var data = getSignal("AUTO_PLAY");
                if (data != null)
                {
                    foreach (var sig in data)
                    {
                        EffectManager.Instance.Execute(sig.effectKey, sig.arg);
                    }
                }
                foreach (var cid in player.handCard)
                {
                    cards[cid].Hold();
                }
                gameState = CardGameState.PLAYER_PLAY;
                break;
            case CardGameState.PLAYER_PLAY:
                bool toGarbageDeck = cards[cardID].Play();
                if (toGarbageDeck)
                {
                    garbageDeck.Add(cardID);
                }
                player.handCard.Remove(cardID);
                break;
            case CardGameState.TURN_END:
                DiscardAll();
                ClearTurnOnlySignals();
                gameState = CardGameState.TURN_START;
                break;
            case CardGameState.GAME_END:
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextGameState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }
    }

    public void StateChange(List<int> values, int turns)
    {
        throw new NotImplementedException();
    }

    public void PressureChange(int value)
    {
        throw new NotImplementedException();
    }
}
public enum CardGameState
{
    GAME_START,
    TURN_START,
    AUTO_PLAY,
    PLAYER_PLAY,
    TURN_END,
    GAME_END
}

public class CardGameStatistics
{
    public int cardUsed = 0;

    public void Clear()
    {
        cardUsed = 0;
    }
}


