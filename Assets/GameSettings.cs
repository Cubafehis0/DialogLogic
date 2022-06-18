using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSettings : MonoBehaviour
{
    public void BackToMap()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void BackToPlace()
    {
        SceneManager.LoadScene("PlaceScene");
    }
}
