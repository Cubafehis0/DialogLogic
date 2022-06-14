using UnityEngine.EventSystems;

public class SelectRelicObj : RelicObj
{
    public void TestAddThis()
    {
        RelicGameManager.Instance.selectRelicUI.OnChooseRelic();
    }
}