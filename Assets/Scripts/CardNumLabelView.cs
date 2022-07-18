using UnityEngine;
using UnityEngine.UI;

public class CardNumLabelView : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private string format = "{0}/{1}";

    private void Update()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        //int handCnt = CardGameManager.Instance.playerState.CardController.Hand.Count;
        //int maxCnt = CardGameManager.Instance.playerState.Player.PlayerInfo.MaxCardNum;
        //text.text = string.Format(format, handCnt, maxCnt);
    }
}
