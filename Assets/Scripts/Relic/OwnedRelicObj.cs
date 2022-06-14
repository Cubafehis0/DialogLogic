using UnityEngine.EventSystems;
using UnityEngine;
public class OwnedRelicObj : RelicObj
{
    public void TestRemoveThis()
    {
        RelicGameManager.Instance.selectRelicUI.OnRemoveRelic(relic);
        Destroy(gameObject);
    }
}