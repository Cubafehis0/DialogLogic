using JasperMod.SemanticTree;
using ModdingAPI;

public class PlayingPile : Pile<Card>
{
    public PlayingPile() : base()
    {
        OnAdd.AddListener(x => Context.SetCardAlias("From", x.id));
        OnRemove.AddListener(x => Context.SetCardAlias("From", ""));
    }
}
