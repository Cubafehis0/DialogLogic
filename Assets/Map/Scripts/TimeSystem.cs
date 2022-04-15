using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITimeSystem
{
    public void DataPass(int time);
    public void StatePass();
    public void StatePass(int stateDelta);
}
public class TimeSystem : MonoBehaviour, ITimeSystem
{
    [SerializeField]
    private Text showTimeText;

    [SerializeField]
    private int data;
    [SerializeField]
    private TimeState stateTime;

    private static ITimeSystem instance = null;

    public static int TimeStateNum = 2;

    public static ITimeSystem Instance
    {
        get => instance;
    }
    public int Data { get => data; }
    public TimeState StateTime { get => stateTime; }

    private void Awake()
    {
        ShowTime();
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        if (MapScript.Instance != null)
            MapScript.Instance.timePassEvent.AddListener(StatePass);

    }

    public void DataPass(int time)
    {
        this.data += time;
        ShowTime();
    }
    public void StatePass()
    {
        this.data += ((int)this.stateTime + 1) / TimeStateNum;
        this.stateTime = (TimeState)(((int)this.stateTime + 1) % TimeStateNum);
        ShowTime();
    }
    public void StatePass(int stateDelta)
    {
        this.data += ((int)this.stateTime + stateDelta) / TimeStateNum;
        this.stateTime = (TimeState)(((int)this.stateTime + stateDelta) % TimeStateNum);
        ShowTime();
    }
    private void ShowTime()
    {
        if (showTimeText)
        {
            showTimeText.text = "第" + data + "天" + "   " +
                (stateTime == TimeState.Day ? "白天" : "晚上");
        }
    }
}
