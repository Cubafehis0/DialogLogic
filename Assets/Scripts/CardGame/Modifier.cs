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
    public Personality PersonalityLinear { get; set; }
    public float[] PersonalityMul { get; set; }
    public SpeechType? Focus { get; set; }
    public SpeechArt SpeechLinear { get; set; }
    public CostModifier CostModifier { get; set; }
    public EffectList OnTurnStart { get; set; }
    public EffectList OnPlayCard { get; set; }
    public EffectList OnBuff { get; set; }
    public int AdditionalEnergy { get; set; }
    public int AdditionalDraw { get; set; }
    
    [XmlIgnore]
    public bool FocusSpecified { get { return Focus != null; }}
    [XmlIgnore]
    public bool AdditionalEnergySpecified { get { return Focus != null; } }
    [XmlIgnore]
    public bool AdditionalDrawSpecified { get { return Focus != null; } }

    public void Construct() {
        CostModifier.Construct();
        OnTurnStart.Construct();
        OnPlayCard.Construct();
    }
}

public class ModifierGroup : List<Modifier>
{
    public Personality PersonalityLinear
    {
        get 
        {
            Personality res=new Personality();
            foreach(Modifier modifier in this)
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

