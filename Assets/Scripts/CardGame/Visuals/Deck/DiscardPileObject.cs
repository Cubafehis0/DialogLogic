using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DiscardPileObject : MonoBehaviour
{
    [SerializeField]
    private CardController player;
    [SerializeField]
    private float speed = 50f;

    private void OnEnable()
    {
        player.DiscardPile.OnAdd.AddListener(OnAdd);
    }
    private void OnDisable()
    {
        player.DiscardPile.OnRemove.AddListener(OnAdd);
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
        CardObject c = StaticCardLibrary.Instance.GetCardObject(card);
        if (c == null)
        {
            throw new System.NotImplementedException();
        }
        c.transform.SetParent(transform, true);
        AnimationManager.Instance.AddAnimation(Discard(c.gameObject));
    }
}
