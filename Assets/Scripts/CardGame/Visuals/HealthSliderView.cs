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
        healthSlider.value= GameManager.Instance.localPlayer.PlayerInfo.Health;
        sanSlider.value = GameManager.Instance.localPlayer.PlayerInfo.san;
    }

}