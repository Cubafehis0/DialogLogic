using System.Collections.Generic;

public class ModifierGroup : List<Modifier>, IReadonlyModifierGroup
{
    private Personality anonymousPersonality = new Personality();

    public void AddAnonymousPersonality(Personality personality)
    {
        anonymousPersonality += personality;
    }

    public Personality PersonalityLinear
    {
        get
        {
            Personality res = anonymousPersonality;
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

