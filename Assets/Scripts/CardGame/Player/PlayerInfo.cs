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
    private int num;
    [SerializeField]
    private int drawCardNum;
    [SerializeField]
    private int basePressure;
    [SerializeField]
    private int maxPressure;
    [SerializeField]
    private int health;
    [SerializeField]
    private int energy;
    [SerializeField]
    private List<string> cardSet;

    [XmlElement(ElementName = "personality")]
    public Personality Personality { get => personality; set => personality = value; }

    [XmlElement(ElementName = "maxCardNum")]
    public int Num { get => num; set => num = value; }

    [XmlElement(ElementName = "drawCardNum")]
    public int DrawCardNum { get => drawCardNum; set => drawCardNum = value; }

    [XmlElement(ElementName = "basePressure")]
    public int BasePressure { get => basePressure; set => basePressure = value; }

    [XmlElement(ElementName = "maxPressure")]
    public int MaxPressure { get => maxPressure; set => maxPressure = value; }

    [XmlElement(ElementName = "health")]
    public int Health { get => health; set => health = value; }

    [XmlElement(ElementName = "energy")]
    public int Energy { get => energy; set => energy = value; }

    [XmlArray(ElementName = "card")]
    public List<string> CardSet { get => cardSet; set => cardSet = value; }
}
