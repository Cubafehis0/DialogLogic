using ModdingAPI;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public struct PlayerInfo
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

    [XmlElement(ElementName = "personality", IsNullable = false)]
    public Personality Personality { get => personality; set => personality = value; }

    [XmlElement(ElementName = "max_card_num")]
    public int MaxCardNum { get => baseMaxCardNum; set => baseMaxCardNum = value; }

    [XmlElement(ElementName = "draw_num")]
    public int DrawNum { get => drawNum; set => drawNum = value; }

    [XmlElement(ElementName = "max_pressure")]
    public int MaxPressure { get => maxPressure; set => maxPressure = value; }

    [XmlElement(ElementName = "max_health")]
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    [XmlIgnore]
    public int Health { get => health; set => health = value; }

    [XmlElement(ElementName = "everry_turn_energy")]
    public int BaseEnergy { get => baseEnergy; set => baseEnergy = value; }

    [XmlArray(ElementName = "cards")]
    public List<string> CardSet { get => cardSet; set => cardSet = value; }

    [XmlIgnore]
    public int Energy { get => energy; set => energy = value; }

    [XmlIgnore]
    public int Pressure
    {
        get => pressure;
        set => pressure = Mathf.Clamp(value, 0, MaxPressure);
    }

}
