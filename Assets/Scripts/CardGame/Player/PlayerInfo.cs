using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

[Serializable]
public class PlayerInfo
{
    [XmlElement(ElementName = "personality")]
    public Personality Personality { get; set; }
    
    [XmlElement(ElementName = "maxCardNum")]
    public int Num { get; set; }

    [XmlElement(ElementName = "drawCardNum")]
    public int DrawCardNum { get; set; }

    [XmlElement(ElementName = "basePressure")]
    public int BasePressure { get; set; }

    [XmlElement(ElementName = "maxPressure")]
    public int MaxPressure { get; set; }

    [XmlElement(ElementName = "health")]
    public int Health { get; set; }

    [XmlElement(ElementName = "energy")]
    public int Energy { get; set; }

    [XmlArray(ElementName ="card")]
    public List<string> CardSet { get; set; }
}
