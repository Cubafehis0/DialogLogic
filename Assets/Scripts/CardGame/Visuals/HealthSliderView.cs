using System.Collections;
using System.Collections.Generic;
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
        healthSlider.value= GameManager.Instance.LocalPlayer.PlayerInfo.Pressure;
        sanSlider.value = GameManager.Instance.LocalPlayer.PlayerInfo.Health;
    }

}
