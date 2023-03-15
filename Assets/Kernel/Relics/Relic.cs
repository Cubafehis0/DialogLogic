using ModdingAPI;
using System;
using YetAInventory;

[Serializable]
public abstract class Relic : UniqueItem, ICardControllerEventListener<Card>,ICloneable
{
    public string Name;
    public string Title;
    public string Description;
    public int Rarity;
    public PersonalityType Category;
    public int InitNum;

    public Modifier modifier;

    public int counter;
    public static PersonalityType GetOppositeCategory(PersonalityType type)
    {
        return type switch
        {
            PersonalityType.Logic => PersonalityType.Passion,
            PersonalityType.Passion => PersonalityType.Logic,
            PersonalityType.Moral => PersonalityType.Unethic,
            PersonalityType.Unethic => PersonalityType.Moral,
            PersonalityType.Detour => PersonalityType.Strong,
            PersonalityType.Strong => PersonalityType.Detour,
            _ => PersonalityType.Inside,
        };
    }

    public Relic() { }

    public Relic(Relic origin)
    {
        this.Name = origin.Name;
        this.Title = origin.Title;
        this.Description= origin.Description;
        this.Rarity = origin.Rarity;
        this.Category = origin.Category;
        this.InitNum = origin.InitNum;
        this.modifier = origin.modifier;
        this.counter = origin.counter;
    }

    public Relic(RelicInfo info) : this()
    {
        this.Name = info.Name;
        this.Title = info.Title;
        this.Description = info.Description;
        this.Rarity = info.Rarity;
        this.Category = info.Category;
        this.InitNum = info.InitNum;
        this.modifier = info.modifier;
        this.counter = 0;
    }





    public void LoseThis()
    {
        OnLoseThis();
    }
    public virtual void OnPickUp() { }
    public virtual void OnLoseThis() { }

    public virtual void OnVictory() { }

    public virtual void OnBattleStart() { }
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }

    public virtual void OnHealthChange() { }
    public virtual void OnPersonalityChange() { }

    public virtual void OnPlayCard(Card card) { }
    public virtual void OnDraw(Card card) { }
    public virtual void OnDiscard(Card card) { }
    public virtual void OnDiscard2Draw() { }
    public virtual void OnEnergyChange() { }

    public abstract object Clone();
}