using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    [SerializeField]
    private Personality personality = null;
    [SerializeField]
    private int baseMaxCardNum=10;
    [SerializeField]
    private int drawNum=4;
    [SerializeField]
    private int pressure;
    [SerializeField]
    private int maxPressure;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int energy;
    [SerializeField]
    private int everyTurnEnergy;

    [SerializeField]
    private List<string> cardSet;

    [XmlElement(ElementName = "personality")]
    public Personality Personality { get => personality; set => personality = value; }

    [XmlElement(ElementName = "maxCardNum")]
    public int MaxCardNum { get => baseMaxCardNum; set => baseMaxCardNum = value; }

    [XmlElement(ElementName = "drawCardNum")]
    public int DrawNum { get => drawNum; set => drawNum = value; }

    [XmlElement(ElementName = "maxPressure")]
    public int MaxPressure { get => maxPressure; set => maxPressure = value; }

    [XmlElement(ElementName = "health")]
    public int Health { get => maxHealth; set => maxHealth = value; }

    [XmlElement(ElementName = "energy")]
    public int BaseEnergy { get => everyTurnEnergy; set => everyTurnEnergy = value; }

    [XmlArray(ElementName = "card")]
    public List<string> CardSet { get => cardSet; set => cardSet = value; }
    public int Energy { get => energy; set => energy = value; }
    public int Pressure { get => pressure; set => pressure = value; }
}
