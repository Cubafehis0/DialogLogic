using System.Collections;
using UnityEngine;
public class DragHandPileObject : HandObjectBase
{
    [SerializeField]
    private CardPlayerState playerState;
    public override void Add(CardBase item)
    {
        base.Add(item);
        if(item is Card card)
        {
            if (card.info.handModifier != null)
            {
                playerState.Modifiers.Add(card.info.handModifier);
            }
        }
    }

    public override void Remove(CardBase item)
    {
        base.Remove(item);

        if (item is Card card)
        {
            if (card.info.handModifier != null)
            {
                playerState.Modifiers.Remove(card.info.handModifier);
            }
        }
    }
}
