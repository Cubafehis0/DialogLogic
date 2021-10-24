using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardGameManager : MonoBehaviour
{
    public CardTable cardTable;
    
    private static CardGameManager _instance = null;
    public static CardGameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private Dictionary<uint, Card> cards = new Dictionary<uint, Card>();
    //public Player player = new Player();
    public Player player;
    public List<uint> deck = new List<uint>();
    public List<uint> garbageDeck = new List<uint>();
    public CardGameState gameState = CardGameState.GAME_START;
    
    
    private const uint PLAYER_START_CARD_NUM = 5;
    private const uint PLAYER_HAND_CARD_NUM = 10;

    /// <summary>
    /// 供effect与游戏流程中的同步使用
    /// </summary>
    private Dictionary<string, List<Signal>> signalDictionary = new Dictionary<string, List<Signal>>();

    public void setSignal(string key,Signal sig,bool clear = false)
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
    /// 使用时检查null
    /// </summary>
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
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        parseCardData();
        initPlayer();
    }

    private void initPlayer()
    {
        uint[] data = {1000, 1001, 1002, 1003, 1004, 1005,1006};
        player.cardSet.AddRange(data);
    }
    private void parseCardData()
    {
        foreach (CardEntity entity in cardTable.Sheet1)
        {
            cards.Add(entity.id,
                new Card(entity.id, entity.hold_effect,entity.hold_effect_scale, entity.condition,entity.condition_scale,entity.effect,entity.effect_scale,entity.post_effect,entity.post_effect_scale));
        }
    }


    public void draw(uint num)
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

    private void discardAll()
    {
        garbageDeck.AddRange(player.handCard);
        player.handCard.Clear();
    }
    /// <summary>
    /// 结束本局游戏
    /// </summary>
    public void endGame()
    {
        deck.Clear();
        garbageDeck.Clear();
        // TODO : implement effect queue
        gameState = CardGameState.GAME_END;
        nextGameState();
    }
    public void nextGameState(int cardIndexInHand = 0)
    {
        switch (gameState)
        {
            case CardGameState.GAME_START:
                Debug.Log("GAME START!");
                deck.Clear();
                garbageDeck.Clear();
                deck.AddRange(player.cardSet);
                Shuffle(deck);
                player.handCard.Clear();
                gameState = CardGameState.TURN_START;
                break;
            case CardGameState.TURN_START:
                Debug.Log("TURN START!");
                player.originData.CopyTo(player.data,0);
                draw(PLAYER_START_CARD_NUM);
                Debug.Log("MY TURN, DRAW!");
                gameState = CardGameState.AUTO_PLAY;
                nextGameState(cardIndexInHand);
                break;
            case CardGameState.AUTO_PLAY:
                var data = getSignal("AUTO_PLAY");
                if (data != null)
                {
                    foreach (var sig in data)
                    {
                        EffectManager.Instance.execute(sig.effectKey, sig.arg);
                    }
                }
                foreach (var cid in player.handCard)
                {
                    cards[cid].hold();
                }
                gameState = CardGameState.PLAYER_PLAY;
                break;
            case CardGameState.PLAYER_PLAY:
                bool toGarbageDeck = cards[player.handCard[cardIndexInHand]].play();
                if (toGarbageDeck)
                {
                    garbageDeck.Add(player.handCard[cardIndexInHand]);
                }
                player.handCard.RemoveAt(cardIndexInHand);
                gameState = CardGameState.TURN_END;
                break;
            case CardGameState.TURN_END:
                discardAll();
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
            nextGameState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            endGame();
        }
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
public class Card
{
    public uint id;
    public List<string> hold_effect;
    public List<string> condition;
    public List<string> effect;
    public List<string> post_effect;
    
    public List<int> hold_effect_scale;
    public List<int> condition_scale;
    public List<int> effect_scale;
    public List<int> post_effect_scale;
    public Card(uint id, string hold_effect,string hold_effect_scale,string condition,string condition_scale,string effect, string effect_scale,string post_effect,string post_effect_scale)
    {
        this.id = id;
        this.hold_effect = new List<string>(hold_effect.Split(';'));
        this.condition = new List<string>(condition.Split(';'));
        this.effect = new List<string>(effect.Split(';'));
        this.post_effect = new List<string>(post_effect.Split(';'));

        this.hold_effect_scale = new List<int>(hold_effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.condition_scale = new List<int>(condition_scale.Split(';').Select(x => Int32.Parse(x)));
        this.effect_scale = new List<int>(effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.post_effect_scale = new List<int>(post_effect_scale.Split(';').Select(x => Int32.Parse(x)));
    }

    public bool play()
    {
        bool res = true;
        if (examine(condition,condition_scale))
        {
            res = examine(effect,effect_scale);
            examine(post_effect,post_effect_scale);
            return res;
        }
        return false;
    }

    public bool hold()
    {
        return examine(hold_effect, hold_effect_scale);
    }
    public bool examine(List<string> effects,List<int> scale)
    {
        bool res = true;
        for (int i = 0; i < effects.Count; i++)
        {
            res = res && EffectManager.Instance.execute(effects[i],scale[i]);
        }
        return res;
    }
    public bool examine(List<string> effects,Character chara)
    {
        bool res = true;
        foreach (var key in effects)
        {
            res = res && EffectManager.Instance.execute(key,chara);
        }
        return res;
    }
}

