using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationManager : MonoBehaviour
{
    private Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();

    private static AnimationManager instance;
    public static AnimationManager Instance {get {return instance;}}

    private void Awake()
    {
        instance = this;
    }
    public void AddAnimation(IEnumerator enumerator)
    {
        animationQueue.Enqueue(enumerator);
    }



    private void Start()
    {
        StartCoroutine(Animating());
    }

    IEnumerator Animating()
    {
        while (true)
        {
            if (animationQueue.Count == 0) yield return null;
            else yield return StartCoroutine(animationQueue.Dequeue());
        }
    }

}