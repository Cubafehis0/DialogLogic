using UnityEngine;
using UnityEngine.UI;

public class HealthSliderView : MonoBehaviour
{
    [SerializeField]
    private PlayerPacked target;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider sanSlider;

    private void Update()
    {
        healthSlider.value = target.Pressure;
        sanSlider.value = target.Health;
    }

}
