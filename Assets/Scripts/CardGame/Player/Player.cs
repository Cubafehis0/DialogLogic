using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int Health { get; set; }
    /// <summary>
    /// 当前压力
    /// </summary>
    int Pressure { get; set; }
    /// <summary>
    /// 可承受压力总值
    /// </summary>
    int PressureSum { get; set; }
    int[] VanilaCharacter { get; }
    List<int> CardSet { get; }
}

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField]
    private int health;
    public int Health { get => health; set => health = value; }
    [SerializeField]
    private int pressure;
    public int Pressure { get => pressure; set => pressure = value; }
    [SerializeField]
    private int pressureSum;
    public int PressureSum { get => pressureSum; set => pressureSum = value; }

    private List<int> cardSet = new List<int>();
    public List<int> CardSet { get => cardSet; }

    [SerializeField]
    private int[] vanilaCharacter = PlayerLibrary.NeutralCharacter;
    public int[] VanilaCharacter { get => vanilaCharacter.Clone() as int[]; }
}
