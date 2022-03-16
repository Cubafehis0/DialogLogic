using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiscardPileObject : PileObject
{
    public static DiscardPileObject instance = null;
    [SerializeField]
    private float speed = 50f;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void OnEnable()
    {
        pile = CardPlayerState.Instance.DiscardPile;
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

    protected override void OnAdd(Card card)
    {
        base.OnAdd(card);
        StartCoroutine(Discard(card));
    }
}
