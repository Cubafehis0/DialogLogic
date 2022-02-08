using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CardGameManager.Instance.NextGameState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //CardGameManager.Instance.EndGame();
        }
    }
}
