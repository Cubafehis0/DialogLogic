using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class Common
{
    [XmlElement(ElementName = "define_card")]
    public List<CardInfo> CardInfos;
    [XmlElement(ElementName = "define_status")]
    public List<Status> Statuss;
    public void Declare()
    {
        CardInfos?.ForEach(x => StaticCardLibrary.Instance.DeclareCard(x.Name));
        Statuss?.ForEach(x => StaticStatusLibrary.DeclareStatus(x.Name));
    }

    public void Define()
    {
        CardInfos?.ForEach(x => StaticCardLibrary.Instance.DefineCard(x));
        Statuss?.ForEach(x => x.Construct());
    }
}
