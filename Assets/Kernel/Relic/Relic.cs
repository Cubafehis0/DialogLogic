using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum RarityType
{
    COMMON,
    RARE
}

public class Relic
{
    public string identification;
    public string name;
    public string desc;
    public string icon;

    public RarityType rarity;
    public PersonalityType category;

    public Modifier bindingModifier;
}
