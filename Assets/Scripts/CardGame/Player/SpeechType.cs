using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
public enum SpeechType
{
    [XmlEnum(Name ="normal")]
    Normal=3,
    [XmlEnum(Name = "cheat")]
    Cheat=0,
    [XmlEnum(Name = "threaten")]
    Threaten=2,
    [XmlEnum(Name = "persuade")]
    Persuade=1
}
