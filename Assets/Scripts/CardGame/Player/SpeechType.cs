using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
public enum SpeechType
{
    [XmlEnum(Name ="normal")]
    Normal,
    [XmlEnum(Name = "cheat")]
    Cheat,
    [XmlEnum(Name = "threaten")]
    Threaten,
    [XmlEnum(Name = "persuade")]
    Persuade
}
