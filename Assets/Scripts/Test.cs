using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Playables;
public class Test : MonoBehaviour,IPlayableAsset
{
    public double duration => throw new System.NotImplementedException();

    public IEnumerable<PlayableBinding> outputs => throw new System.NotImplementedException();

    public Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        GameObject go = new GameObject();
        go.AddComponent<StaticCardLibrary>();
        go.AddComponent<Player>();
        go.AddComponent<GameManager>();
        //GameManager.Instance.LocalPlayer.PlayerInfo.CardSet = new List<string> { };
    }
}
