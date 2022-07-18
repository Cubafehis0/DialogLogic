using UnityEngine;
using UnityEngine.UI;
public class TestSwitchMan : MonoBehaviour
{
    public Image image;

    public int index = 0;

    public Sprite[] t;

    public void Next()
    {
        image.sprite = t[index++];
        if (index == t.Length)
        {
            index = 0;
        }
    }
}
