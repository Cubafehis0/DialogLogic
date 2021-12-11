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
}

public class Character : MonoBehaviour, ICharacter
{
    private int[] personality = new int[4];
    public int[] Personality { get => personality; set => personality = value; }

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
}

