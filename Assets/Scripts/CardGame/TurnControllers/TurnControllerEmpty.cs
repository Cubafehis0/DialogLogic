public class TurnControllerEmpty : TurnController
{
    public override bool EndTurnTrigger => true;

    public override void EndTurn() { }
}
