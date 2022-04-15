using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaceScript : MonoBehaviour
{

    [SerializeField]
    private PlaceIncident placeIncident = null;
    [SerializeField]
    private string placeName;
    [SerializeField]
    private Button startButton;
    private void Awake()
    {
        placeIncident = new PlaceIncident(placeName);
        startButton.onClick.AddListener(OnClickStartButton);
    }

    public void OnClickStartButton()
    {
        if (placeIncident != null)
        {
            string s = placeIncident.GetIncident();
            SceneManager.LoadScene(s);
        }
        else
            Debug.Log(name + "lost placeIncident");
    }
}
