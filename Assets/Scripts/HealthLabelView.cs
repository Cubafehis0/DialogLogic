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
        var playerInfo = GameManager.Instance.LocalPlayer;
        pressureText.text = string.Format("{0}/{1}", playerInfo.Pressure, playerInfo.MaxPressure);
        sanText.text = string.Format("{0}/{1}", playerInfo.Health, playerInfo.MaxHealth);
    }
}
