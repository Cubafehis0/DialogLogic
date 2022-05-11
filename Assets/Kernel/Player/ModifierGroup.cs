using System.Collections.Generic;

public class ModifierGroup : List<Modifier>, IReadonlyModifierGroup
{
    private Modifier anonymous=new Modifier();

    public void AddAnonymousPersonality(Personality personality)
    {
        anonymous.PersonalityLinear += personality;
    }

    public Personality PersonalityLinear
    {
        get
        {
            Personality res = anonymous.PersonalityLinear;
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

    public void OnTurnStart()
    {
        foreach (var modifier in this)
        {
            modifier.OnTurnStart?.Execute();
        }
    }

    public void OnTurnEnd()
    {
        
    }

    public void OnPlayCard()
    {
        foreach (var modifier in this)
        {
            modifier.OnPlayCard?.Execute();
        }
    }

    public void OnBuff()
    {
        foreach (var modifier in this)
        {
            modifier.OnPlayCard?.Execute();
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

