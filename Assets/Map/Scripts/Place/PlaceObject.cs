using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceObject : MonoBehaviour
{
    public Place place = null;

    private void OnMouseUpAsButton()
    {
        Debug.Log("click");
    }
}
