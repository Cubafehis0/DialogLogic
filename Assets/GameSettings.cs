using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSettings : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    private void Start()
    {
        Close();
    }
    public void Open()
    {
        panel.SetActive(true);
    }
    public void Close()
    {
        panel.SetActive(false);
    }
    public void BackToMap()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void BackToPlace()
    {
        SceneManager.LoadScene("PlaceScene");
    }
}
