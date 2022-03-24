using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health;
    public int Health { get => health; set => health = value; }

    [SerializeField]
    private List<string> cardSet = new List<string>();
    public List<string> CardSet { get => cardSet; }

    [SerializeField]
    private int[] vanilaCharacter = PlayerLibrary.NeutralCharacter;
    public int[] VanilaCharacter { get => vanilaCharacter.Clone() as int[]; }
}
