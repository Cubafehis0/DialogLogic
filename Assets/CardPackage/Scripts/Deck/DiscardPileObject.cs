using System.Collections;
using UnityEngine;

public class DiscardPileObject : PilePacked
{
    [SerializeField]
    private float speed = 50f;

    private void OnEnable()
    {
        OnAdd.AddListener(PlayDiscardAnim);
    }
    private void OnDisable()
    {
        OnAdd.RemoveListener(PlayDiscardAnim);
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

    private void PlayDiscardAnim(CardBase card)
    {
        CardObjectBase c = Singleton<DynamicLibrary>.Instance.GetCardObject(card);
        if (c == null)
        {
            throw new System.NotImplementedException();
        }
        c.transform.SetParent(transform, true);
        AnimationManager.Instance.AddAnimation(Discard(c.gameObject));
    }
}
