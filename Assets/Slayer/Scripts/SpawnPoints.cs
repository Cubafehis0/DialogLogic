using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField]
    private Transform[] beacons=new Transform[10];

    private void OnDrawGizmos()
    {
        //for(int i = 0; i < beacons.Length; i++)
        //{
        //    Transform t = beacons[i];
        //    if (t != null)
        //    {
        //        Handles.Label(t.position, i.ToString());
        //    }
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
