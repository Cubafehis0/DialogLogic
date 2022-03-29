using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiscardPileObject : MonoBehaviour
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
        CardPlayerState.Instance.DiscardPile.OnAdd.AddListener(OnAdd);
    }
    private void OnDisable()
    {
        CardPlayerState.Instance.DiscardPile.OnRemove.AddListener(OnAdd);
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

    private void OnAdd(Card card)
    {
        card.transform.SetParent(transform, true);
        StartCoroutine(Discard(card));
    }
}
