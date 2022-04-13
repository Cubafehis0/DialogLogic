using SemanticTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

[Serializable]
public class Modifier
{
    [XmlElement(ElementName = "personality_linear")]
    public Personality PersonalityLinear { get; set; } = new Personality();

    [XmlElement(ElementName = "personality_factor")]
    public float[] PersonalityMul { get; set; }

    [XmlElement(ElementName = "focus")]
    public SpeechType? Focus { get; set; }

    [XmlElement(ElementName = "speech")]
    public SpeechArt SpeechLinear { get; set; }

    [XmlElement(ElementName = "cost_modifier")]
    public CostModifier CostModifier { get; set; }

    [XmlElement(ElementName = "on_turn_start")]
    public EffectList OnTurnStart { get; set; }

    [XmlElement(ElementName = "on_play_card")]
    public EffectList OnPlayCard { get; set; }

    [XmlElement(ElementName = "on_buff")]
    public EffectList OnBuff { get; set; }

    [XmlElement(ElementName = "additional_energy")]
    public int AdditionalEnergy { get; set; }

    [XmlElement(ElementName = "additional_draw")]
    public int AdditionalDraw { get; set; }

    [XmlIgnore]
    public bool FocusSpecified { get { return Focus != null; } }
    [XmlIgnore]
    public bool AdditionalEnergySpecified { get { return Focus != null; } }
    [XmlIgnore]
    public bool AdditionalDrawSpecified { get { return Focus != null; } }

    public void Construct()
    {
        CostModifier?.Construct();
        OnTurnStart?.Construct();
        OnPlayCard?.Construct();
    }
}

public interface IReadonlyModifierGroup : IReadOnlyList<Modifier>
{
    Personality PersonalityLinear { get; }
    SpeechType? Focus { get; }
    SpeechArt SpeechLinear { get; }
    int AdditionalEnergy { get; }
    int AdditionalDraw { get; }
}

public class ModifierGroup : List<Modifier>, IReadonlyModifierGroup
{
    public Personality PersonalityLinear
    {
        get
        {
            Personality res = new Personality();
            foreach (Modifier modifier in this)
            {
                var entry = modifier.PersonalityLinear;
                if (entry != null)
                    res += entry;
            }
            return res;
        }
    }
    public SpeechType? Focus
    {
        get
        {
            SpeechType? res = null;
            foreach (Modifier modifier in this)
            {
                var entry = modifier.Focus;
                if (entry != null)
                    res = entry;
            }
            return res;
        }
    }
    public SpeechArt SpeechLinear
    {
        get
        {
            SpeechArt res = new SpeechArt();
            foreach (Modifier modifier in this)
            {
                var entry = modifier.SpeechLinear;
                if (entry != null)
                    res += entry;
            }
            return res;
        }
    }
    public int AdditionalEnergy
    {
        get
        {
            int res = 0;
            foreach (Modifier modifier in this)
            {
                var entry = modifier.AdditionalEnergy;
                res += entry;
            }
            return res;
        }
    }
    public int AdditionalDraw
    {
        get
        {
            int res = 0;
            foreach (Modifier modifier in this)
            {
                var entry = modifier.AdditionalDraw;
                res += entry;
            }
            return res;
        }
    }
}

