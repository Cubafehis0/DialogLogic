using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTime : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField]
    DisplayGameObjectAnim displayGameObjectAnim;
    public void HideGameObjects()
    {
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
    }

    public void DisplayGameObjects()
    {
        displayGameObjectAnim = gameObject.GetComponent<DisplayGameObjectAnim>();
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
