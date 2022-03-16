using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink2Unity;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CardGameManager.Instance.StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}