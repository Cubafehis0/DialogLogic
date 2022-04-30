using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    [SerializeField]
    private Personality personality = null;
    [SerializeField]
    private int baseMaxCardNum = 10;
    [SerializeField]
    private int drawNum = 4;
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
    private int everyTurnEnergy;
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
    public int BaseEnergy { get => everyTurnEnergy; set => everyTurnEnergy = value; }

    [XmlArray(ElementName = "cards")]
    public List<string> CardSet { get => cardSet; set => cardSet = value; }

    [XmlIgnore]
    public int Energy { get => energy; set => energy = value; }

    [XmlIgnore]
    public int Pressure { get => pressure; set => pressure = value; }

}
