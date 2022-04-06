using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceScript : MonoBehaviour
{

    [SerializeField]
    private PlaceIncident placeIncident = null;
    [SerializeField]
    private string placeName;
    private void Awake()
    {
        placeIncident = new PlaceIncident(placeName);
    }

    public void OnMouseDown()
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
