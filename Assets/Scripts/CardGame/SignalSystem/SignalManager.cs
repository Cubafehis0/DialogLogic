
using System;
using System.Collections.Generic;
using UnityEngine;


public interface ISignalManager
{
    void SetSignal(string key, Signal sig);
}

public class SignalManager : MonoBehaviour, ICardGameListener,ISignalManager
{
    private static SignalManager instance = null;
    public static SignalManager Instance { get => instance; }

    /// <summary>
    /// 供effect与游戏流程中的同步使用
    /// </summary>
    private Dictionary<string, List<Signal>> signalDictionary = new Dictionary<string, List<Signal>>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    /// <summary>
    /// 设置信息
    /// </summary>
    /// <param name="key">时间点</param>
    /// <param name="sig">信息</param>
    /// <param name="clear">是否清除</param>
    public void SetSignal(string key, Signal sig)
    {
        if (signalDictionary.ContainsKey(key))
        {
            signalDictionary[key].Add(sig);
        }
        else
        {
            signalDictionary.Add(key, new List<Signal>());
        }
    }


    public void OnStartTurn()
    {
        if (signalDictionary.TryGetValue("AUTO_PLAY", out List<Signal> signals))
        {
            foreach (var sig in signals)
            {
                sig.lastTurns--;
                if (sig.lastTurns == 0)
                {
                    signals.Remove(sig);
                }
            }
        }
    }

    public void OnEndTurn()
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

    public void OnStartGame()
    {
        return;
    }
}





