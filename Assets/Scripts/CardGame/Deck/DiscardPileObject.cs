using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pile))]
public class DiscardPileObject : MonoBehaviour, IPileListener
{
    public static DiscardPileObject instance = null;


    float speed = 50f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void OnEnable()
    {
        GetComponent<IPile>().UpdateBindingObject();
    }

    private void OnDisable()
    {
        GetComponent<IPile>().UpdateBindingObject();
    }

    IEnumerator Discard(Card card)
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
        card.gameObject.SetActive(false);
    }

    public void OnAdd(Card card)
    {
        card.transform.SetParent(transform, true);
        if (card) StartCoroutine(Discard(card));
    }

    public void OnRemove(Card oldCard)
    {
        return;
    }

    public void OnShuffle()
    {
        return;
    }
}
