using ModdingAPI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPacked : MonoBehaviour, IProperties
{
    [SerializeField]
    private Personality personality;
    [SerializeField]
    private int baseMaxCardNum;
    [SerializeField]
    private int drawNum;
    [SerializeField]
    private int pressure;
    [SerializeField]
    private int maxPressure;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int baseEnergy;
    [SerializeField]
    private List<string> cardSet;

    public Personality Personality { get => personality; set => personality = value; }
    public int MaxCardNum { get => baseMaxCardNum; set => baseMaxCardNum = value; }
    public int DrawNum { get => drawNum; set => drawNum = value; }
    public int MaxPressure { get => maxPressure; set => maxPressure = value; }
    public int Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int BaseEnergy { get => baseEnergy; set => baseEnergy = value; }
    public List<string> CardSet { get => cardSet; set => cardSet = value; }
    public int Energy { get => energy; set => energy = value; }
    public int Pressure { get => pressure; set => pressure = Mathf.Clamp(value, 0, MaxPressure); }

    public void Load(PlayerInfo info)
    {
        this.personality = info.Personality;
        this.baseMaxCardNum = info.MaxCardNum;
        this.drawNum = info.DrawNum;
        this.pressure = info.Pressure;
        this.maxPressure = info.MaxPressure;
        this.maxHealth = info.MaxHealth;
        this.health = info.Health;
        this.energy = info.Energy;
        this.baseEnergy = info.Energy;
        this.cardSet = info.CardSet;
    }

    public bool TryGetInt(string key, out int value)
    {
        switch (key)
        {
            case "inner":
                value = personality[PersonalityType.Inside];
                return true;
            case "outside":
                value = personality[PersonalityType.Outside];
                return true;
            case "logic":
                value = personality[PersonalityType.Logic];
                return true;
            case "spirital":
                value = personality[PersonalityType.Passion];
                return true;
            case "moral":
                value = personality[PersonalityType.Moral];
                return true;
            case "immoral":
                value = personality[PersonalityType.Unethic];
                return true;
            case "roundabout":
                value = personality[PersonalityType.Detour];
                return true;
            case "aggressive":
                value = personality[PersonalityType.Strong];
                return true;
            default:
                value = 0;
                return false;
        }

    }
}
