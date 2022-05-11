public class HandPile : Pile<Card>
{
    private ModifierGroup modifiers;
    public IReadonlyModifierGroup Modifiers { get { return modifiers; } }

    public HandPile() : base()
    {
        OnAdd.AddListener(x =>
        {
            if (x.info.handModifier != null)
                modifiers.Add(x.info.handModifier);
        });
        OnRemove.AddListener(x =>
        {
            if (x.info.handModifier != null)
                modifiers.Remove(x.info.handModifier);
        });
    }

}
