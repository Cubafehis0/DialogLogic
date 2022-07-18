using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationManager : Singleton<AnimationManager>
{
    private Queue<IEnumerator> animationQueue = new Queue<IEnumerator>();

    public void AddAnimation(IEnumerator enumerator)
    {
        animationQueue.Enqueue(enumerator);
    }

    public bool IsFree()
    {
        return true;
        //return ForegoundGUISystem.current == false;
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