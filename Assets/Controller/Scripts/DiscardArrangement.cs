using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DiscardArrangement : MonoBehaviour,IDeckEventHandler
{
    public static DiscardArrangement instance = null;


    float speed = 50f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    IEnumerator Discard(CardObject card)
    {
        float jumpHeight = Random.Range(1f, 2f);
        float ax = 0f;
        while (ax < jumpHeight)
        {
            ax += Time.deltaTime * speed;
            card.transform.Translate(Time.deltaTime * speed * Vector2.up);
            yield return null;
        }
        while (Vector2.Distance(card.transform.position, transform.position) > 0.01f)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, transform.position, Time.deltaTime * speed);
            yield return null;
        }
        Destroy(card.gameObject, 0.1f);
    }

    public void OnDeckUpdate()
    {
        return;
    }

    public void OnDeckAdd(CardObject card)
    {
        if (card) StartCoroutine(Discard(card));
    }

}
