using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardGameState
{
    GAME_START,
    TURN_START,
    AUTO_PLAY,
    PLAYER_PLAY,
    TURN_END,
    GAME_END
}

public interface ICardGameManager
{
    public ICardPlayerState MainPlayerState { get; }
    //CardGameStat Statistics { get; }
    void StartGame();
    void EndTurn();
    void StartTurn();
}

public interface ICardGameListener
{
    void OnStartGame();
    void OnStartTurn();
    void OnEndTurn();
}


public class CardGameManager : MonoBehaviour, ICardGameManager
{
    private static ICardGameManager instance = null;
    public static ICardGameManager Instance
    {
        get => instance;
    }


    [SerializeField]
    private CardPlayerState mainPlayerState;
    public ICardPlayerState MainPlayerState { get => mainPlayerState; }

    /// <summary>
    /// ��Ϸͳ������
    /// </summary>
    //[SerializeField]
    //private CardGameStat statistics = new CardGameStat();
    //public CardGameStat Statistics { get => statistics; }

    private List<ICardGameListener> listeners = new List<ICardGameListener>();

    void Awake()
    {
        instance = this;
        listeners = new List<ICardGameListener>(GetComponents<ICardGameListener>());
    }

    private void Start()
    {
        if (!listeners.Contains(SignalManager.Instance)) listeners.Add(SignalManager.Instance);
        if (!listeners.Contains(CardPlayerState.Instance)) listeners.Add(CardPlayerState.Instance);
    }

    /// <summary>
    /// ����һ����Ϸ
    /// </summary>
    public void StartGame()
    {
        foreach (var listener in listeners) listener.OnStartGame();
    }
    /// <summary>
    /// ������ǰ�غ�
    /// </summary>
    public void EndTurn()
    {
        foreach (var listener in listeners) listener.OnEndTurn();
    }

    /// <summary>
    /// ����һ���غ�
    /// </summary>
    public void StartTurn()
    {
        foreach (var listener in listeners) listener.OnStartTurn();
    }
}
