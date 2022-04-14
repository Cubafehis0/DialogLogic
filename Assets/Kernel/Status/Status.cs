using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using SemanticTree;

public class Status
{

    [XmlElement(ElementName = "name", IsNullable = true)]
    public string Name;

    [XmlIgnore]
    public int DecreaseOnTurnStart = 0;

    [XmlElement(ElementName = "decrease_on_turn_end")]
    public int DecreaseOnTurnEnd = 0;

    [XmlIgnore]
    public bool Stackable = true;

    [XmlIgnore]
    public bool AllowNegative = false;

    [XmlElement(ElementName = "binding_modifier")]
    public Modifier Modifier = null;

    [XmlElement(ElementName = "on_add")]
    public EffectList OnAdd = null;

    [XmlElement(ElementName = "on_remove")]
    public EffectList OnRemove = null;

    [XmlIgnore]
    public bool DecreaseOnTurnEndSpecified { get { return DecreaseOnTurnEnd != 0; } set { } }

    public void Construct()
    {
        OnAdd?.Construct();
        OnRemove?.Construct();
        Modifier?.Construct();
    }
}

[Serializable]
public class StatusCounter
{
    public Status status = null;
    public int value = 0;
}