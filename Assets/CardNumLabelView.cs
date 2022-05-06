using System.Collections;
using System.Collections.Generic;
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
        text.text = string.Format(format, CardGameManager.Instance.playerState.Hand.Count, CardGameManager.Instance.playerState.Player.PlayerInfo.MaxCardNum);
    }
}
