using UnityEngine;
using UnityEngine.UI;
public class EnergyLabelView : MonoBehaviour
{
    [SerializeField]
    private PlayerPacked player;
    [SerializeField]
    private Text text;

    private void Update()
    {
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (text) text.text = player.Energy.ToString();
    }
}
