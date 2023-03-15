using UnityEngine;
using UnityEngine.UI;

public class EnergyView : MonoBehaviour
{
    [SerializeField]
    private PlayerPacked player;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Image image;

    private void Update()
    {
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (image) image.sprite = sprites[Mathf.Clamp(player.Energy, 0, sprites.Length - 1)];
    }
}
