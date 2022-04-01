using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObjectPool : MonoBehaviour, IPool<GameObject>
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private uint maxSize = 40;
    [SerializeField]
    private uint waterLine = 5;
    private int ObjectCount { get=>objectPool.Count;}
    Queue<GameObject> avaiable;
    List<GameObject> objectPool;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        avaiable = new Queue<GameObject>();
        objectPool = new List<GameObject>();
        for (int i = 0; i < waterLine; i++)
        {
            var go = CreateGameObject();
            objectPool.Add(go);
            avaiable.Enqueue(go);
        }
    }

    private void Dispose(GameObject poolObject)
    {
        objectPool.Remove(poolObject);
        DestroyImmediate(poolObject);
    }
    private GameObject CreateGameObject()
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.parent = transform;
        go.SetActive(false);
        return go;
    }
    /// <summary>
    ///   从对象池中请求一个对象
    /// </summary>
    public GameObject Request()
    {
        if (avaiable.Count < waterLine)
        {
            for(int i=avaiable.Count;i<waterLine;i++)
            {
                var go = CreateGameObject();
                objectPool.Add(go);
                avaiable.Enqueue(go);
            }
        }
        var res=avaiable.Dequeue();
        res.SetActive(true);
        res.transform.parent = null;
        return res;

    }
    /// <summary>
    ///  向对象池归还对象以便重复利用，归还时确保程序的其他地方不存在该对象的引用
    /// </summary>
    public void Recycle(GameObject poolObject)
    {
        if (!objectPool.Contains(poolObject)) return;
        if(ObjectCount>maxSize)
        {
            Dispose(poolObject);
            return;
        }
        else
        {
            avaiable.Enqueue(poolObject);
            poolObject.gameObject.SetActive(false);
            poolObject.transform.parent = transform;
        }
    }
}
