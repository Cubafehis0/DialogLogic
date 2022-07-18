using UnityEngine;
using UnityEngine.UI;

public class HealthSliderView : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider sanSlider;

    private void Update()
    {
        healthSlider.value = CardGameManager.Instance.playerState.Player.PlayerInfo.Pressure;
        sanSlider.value = CardGameManager.Instance.playerState.Player.PlayerInfo.Health;
    }

}
