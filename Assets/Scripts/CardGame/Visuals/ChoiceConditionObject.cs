using System.Collections;
using UnityEngine;

public class ChoiceConditionObject : MonoBehaviour
{

    private PersonalityType type;
    private int value;
    private bool reveal;
    public PersonalityType Type { get => type; set => type = value; }
    public int Value { get => value; set => this.value = value; }
    public bool Reveal { get => reveal; set => reveal = value; }

    public void UpdateVisuals()
    {

    }
}