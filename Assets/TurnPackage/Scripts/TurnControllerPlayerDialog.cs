public class TurnControllerPlayerDialog : TurnController
{
    public override bool EndTurnTrigger => endTurnTrigger || tempEndTurnTrigger;
    private bool endTurnTrigger = false;
    private bool tempEndTurnTrigger = false;
    public void SetEndTurnTrigger() { endTurnTrigger = true; }

    public override bool AdditionalTurn => tempEndTurnTrigger;
    public void GetAddditionalTurnAndEndTurn()
    {
        GameManager.Instance.LocalPlayer.Pressure++;
        tempEndTurnTrigger = true;
    }

    public override void OnStartTurn()
    {
        endTurnTrigger = false;
        tempEndTurnTrigger = false;
        var chooseController = CardGameManager.Instance.playerState.GetComponent<ChooseController>();
        chooseController.SetChoices(CardGameManager.Instance.dialogSystem.GetInkStory().CurrentChoices);
        GUISystemManager.Instance.chooseSystem.gameObject.SetActive(true);
        base.OnStartTurn();
    }
}
