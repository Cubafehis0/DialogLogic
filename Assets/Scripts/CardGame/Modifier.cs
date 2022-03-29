using SemanticTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Modifier
{
    public Personality PersonalityLinear;
    public float[] PersonalityMul;
    public SpeechType Focus;
    public SpeechArt SpeechLinear;
    public CostModifier CostModifier;
    public EffectList OnTurnStart;
    public EffectList OnPlayCard;

    public void Construct() { }
}

