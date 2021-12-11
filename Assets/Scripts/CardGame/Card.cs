using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card : MonoBehaviour
{
    public readonly uint staticID;
    public IPile parentPile;

    public List<string> hold_effect;
    public List<string> condition;
    public List<string> effect;
    public List<string> post_effect;

    public List<int> hold_effect_scale;
    public List<int> condition_scale;
    public List<int> effect_scale;
    public List<int> post_effect_scale;


    public string title;
    public string desc;
    public string meme;

    public Card(uint staticID, string hold_effect, string hold_effect_scale, string condition, string condition_scale, string effect, string effect_scale, string post_effect, string post_effect_scale)
    {
        this.staticID = staticID;
        this.hold_effect = new List<string>(hold_effect.Split(';'));
        this.condition = new List<string>(condition.Split(';'));
        this.effect = new List<string>(effect.Split(';'));
        this.post_effect = new List<string>(post_effect.Split(';'));

        this.hold_effect_scale = new List<int>(hold_effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.condition_scale = new List<int>(condition_scale.Split(';').Select(x => Int32.Parse(x)));
        this.effect_scale = new List<int>(effect_scale.Split(';').Select(x => Int32.Parse(x)));
        this.post_effect_scale = new List<int>(post_effect_scale.Split(';').Select(x => Int32.Parse(x)));
    }

    public Card(uint staticID, List<string> hold_effect, List<int> hold_effect_scale, List<string> condition, List<int> condition_scale, List<string> effect, List<int> effect_scale, List<string> post_effect, List<int> post_effect_scale)
    {
        this.staticID = staticID;
        this.hold_effect = new List<string>(hold_effect);
        this.condition = new List<string>(condition);
        this.effect = new List<string>(effect);
        this.post_effect = new List<string>(post_effect);

        this.hold_effect_scale = new List<int>(hold_effect_scale);
        this.condition_scale = new List<int>(condition_scale);
        this.effect_scale = new List<int>(effect_scale);
        this.post_effect_scale = new List<int>(post_effect_scale);
    }

}