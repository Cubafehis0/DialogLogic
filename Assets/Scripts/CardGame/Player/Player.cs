using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    int Health { get; set; }
    int[] VanilaCharacter { get; }
    List<int> CardSet { get; }
}

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField]
    private int health;
    public int Health { get => health; set => health = value; }

    private List<int> cardSet = new List<int>();
    public List<int> CardSet { get => cardSet; }

    [SerializeField]
    private int[] vanilaCharacter = PlayerLibrary.NeutralCharacter;
    public int[] VanilaCharacter { get => vanilaCharacter.Clone() as int[]; }
}
