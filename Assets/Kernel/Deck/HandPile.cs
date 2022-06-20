using System;

[Serializable]
public class HandPile : Pile<Card>
{
    private ModifierGroup modifiers = new ModifierGroup();
    public ModifierGroup Modifiers { get { return modifiers; } }

    public PileType type;
    public override void Add(Card item)
    {
        base.Add(item);
        if (type == PileType.Hand && item.info.handModifier != null)
        {
            modifiers.Add(item.info.handModifier);
        }
    }

    public override void Remove(Card item)
    {
        base.Remove(item);
        if (type == PileType.Hand && item.info.handModifier != null)
        {
            modifiers.Remove(item.info.handModifier);
        }
    }

}
