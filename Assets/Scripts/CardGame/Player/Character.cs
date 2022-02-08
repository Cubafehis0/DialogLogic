using System;
using UnityEngine;
using UnityEngine.Events;

public interface ICharacter
{
    UnityEvent OnValueChange { get; }
    int[] Personality { get; }
    int Inside { get; set; }
    int Outside { get; set; }
    int Logic { get; set; }
    int Passion { get; set; }
    int Moral { get; set; }
    int Unethic { get; set; }
    int Detour { get; set; }
    int Strong { get; set; }
    int Health { get; set; }
    /// <summary>
    /// 当前压力
    /// </summary>
    int Pressure { get; set; }
    /// <summary>
    /// 可承受压力总值
    /// </summary>
    int PressureSum { get; set; }
}

public enum Personality
{

}

public class Character : MonoBehaviour, ICharacter
{
    [SerializeField]
    private int[] personality = new int[4];
    public int[] Personality { get => personality; set => personality = value; }

    [SerializeField]
    private int health;
    [SerializeField]
    private int pressure;
    [SerializeField]
    private int pressureSum;

    private UnityEvent onValueChange = new UnityEvent();
    public UnityEvent OnValueChange { get => onValueChange; }

    public int Inside
    {
        get => personality[0];
        set
        {
            personality[0] = value;
            OnValueChange.Invoke();
        }
    }
    public int Outside
    {
        get => -personality[0];
        set
        {
            personality[0] = -value;
            OnValueChange.Invoke();
        }
    }
    public int Logic
    {
        get => personality[1];
        set
        {
            personality[1] = value;
            OnValueChange.Invoke();
        }
    }
    public int Passion
    {
        get => -personality[1];
        set
        {
            personality[1] = -value;
            OnValueChange.Invoke();
        }

    }

    public int Moral
    {
        get => personality[2];
        set
        {
            personality[2] = value;
            OnValueChange.Invoke();
        }
    }
    public int Unethic
    {
        get => -personality[2];
        set
        {
            personality[2] = -value;
            OnValueChange.Invoke();
        }
    }

    public int Detour
    {
        get => personality[3];
        set
        {
            personality[3] = value;
            OnValueChange.Invoke();
        }
    }
    public int Strong
    {
        get => -personality[3];
        set
        {
            personality[3] = -value;
            OnValueChange.Invoke();
        }
    }
    public int Health
    {
        get => health;
        set
        {
            health = value;
            OnValueChange.Invoke();
        }
    }
    public int Pressure
    {
        get => pressure;
        set
        {
            pressure = value;
            OnValueChange.Invoke();
        }
    }
    public int PressureSum
    {
        get => pressureSum;
        set
        {
            pressureSum = value;
            OnValueChange.Invoke();
        }
    }
}

