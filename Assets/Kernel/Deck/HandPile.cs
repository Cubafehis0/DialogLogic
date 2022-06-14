public class HandPile : Pile<Card>
{
    private ModifierGroup modifiers = new ModifierGroup();
    public IReadonlyModifierGroup Modifiers { get { return modifiers; } }

    public override void Add(Card item)
    {
        base.Add(item);
        if (item.info.handModifier != null)
            modifiers.Add(item.info.handModifier);
    }

    public override void Remove(Card item)
    {
        base.Remove(item);
        if (item.info.handModifier != null)
            modifiers.Remove(item.info.handModifier);
    }

}
