using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Test : MonoBehaviour
{

    private void Start()
    {
        GameObject go = new GameObject();
        go.AddComponent<StaticCardLibrary>();
        go.AddComponent<Player>();
        go.AddComponent<GameManager>();
        //GameManager.Instance.LocalPlayer.PlayerInfo.CardSet = new List<string> { };
    }
}
