using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLabelView : MonoBehaviour
{
    [SerializeField]
    private Text pressureText;
    [SerializeField]
    private Text sanText;

    private void Update()
    {
        PlayerInfo playerInfo = GameManager.Instance.LocalPlayer.PlayerInfo;
        pressureText.text = string.Format("{0}/{1}",playerInfo.Pressure,playerInfo.MaxPressure);
        sanText.text = string.Format("{0}/{1}", playerInfo.Health, playerInfo.MaxHealth);
    }
}
