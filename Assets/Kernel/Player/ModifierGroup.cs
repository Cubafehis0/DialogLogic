using ModdingAPI;
using System.Collections.Generic;


public class AnonymousModifier
{
    public Personality PersonalityLinear = new Personality();
}



public class ModifierGroup : List<Modifier>
{
    private AnonymousModifier anonymous = new AnonymousModifier();

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
                    res += entry.Invoke();
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
                    res = entry.Invoke();
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
                    res += entry.Invoke();
            }
            return res;
        }
    }

    public void OnTurnStart()
    {
        foreach (var modifier in this)
        {
            modifier.OnTurnStart?.Invoke();
        }
    }

    public void OnTurnEnd()
    {

    }

    public void OnPlayCard()
    {
        foreach (var modifier in this)
        {
            modifier.OnPlayCard?.Invoke();
        }
    }

    public void OnBuff()
    {
        foreach (var modifier in this)
        {
            modifier.OnPlayCard?.Invoke();
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
                res += entry.Invoke();
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
                res += entry.Invoke();
            }
            return res;
        }
    }
}

