using SemanticTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

public class Modifier
{
    public Personality PersonalityLinear;
    public float[] PersonalityMul;
    public SpeechType? Focus;
    public SpeechArt SpeechLinear;
    public CostModifier CostModifier;
    public EffectList OnTurnStart;
    public EffectList OnPlayCard;
    public EffectList OnBuff;

    [XmlIgnore]
    public bool FocusSpecified { get { return Focus != null; } set { } }
    public void Construct() {
        CostModifier.Construct();
        OnTurnStart.Construct();
        OnPlayCard.Construct();
    }
}

