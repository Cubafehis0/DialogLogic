using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PilePacked))]
public class DiscardPileObject : MonoBehaviour
{
    [SerializeField]
    private PilePacked pile;
    [SerializeField]
    private float speed = 50f;

    private void OnEnable()
    {
        pile.OnAdd.AddListener(OnAdd);
    }
    private void OnDisable()
    {
        pile.OnRemove.AddListener(OnAdd);
    }
    IEnumerator Discard(GameObject card)
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
        card.SetActive(false);
    }

    private void OnAdd(Card card)
    {
        CardObject c = GameManager.Instance.CardObjectLibrary.GetCardObject(card);
        if (c == null)
        {
            throw new System.NotImplementedException();
        }
        c.transform.SetParent(transform, true);
        AnimationManager.Instance.AddAnimation(Discard(c.gameObject));
    }
}
