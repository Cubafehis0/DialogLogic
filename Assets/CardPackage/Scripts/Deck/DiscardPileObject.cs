using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiscardPileObject : PilePacked
{
    [SerializeField]
    private float speed = 50f;
    [SerializeField]
    private Text count;
    private void OnEnable()
    {
        OnAdd.AddListener(OnAddAnim);
        OnAdd.AddListener(OnRemoveAnim);
    }
    private void OnDisable()
    {
        OnAdd.RemoveListener(OnAddAnim);
        OnRemove.RemoveListener(OnRemoveAnim);
    }
    private void Start()
    {
        UpdateCount(cards.Count);
    }
    IEnumerator Discard(CardBase card)
    {
        GameObject o = DynamicLibrary.Instance.GetCardObject(card);
        o.transform.SetParent(transform, true);
        float jumpHeight = Random.Range(1f, 2f);
        float ax = 0f;
        while (ax < jumpHeight)
        {
            ax += Time.deltaTime * speed;
            o.transform.Translate(Time.deltaTime * speed * Vector2.up);
            yield return null;
        }
        while (Vector2.Distance(o.transform.position, transform.position) > 0.01f)
        {
            o.transform.position = Vector3.MoveTowards(o.transform.position, transform.position, Time.deltaTime * speed);
            yield return null;
        }
        o.gameObject.SetActive(false);
    }

    private void OnAddAnim(CardBase card)
    {
        UpdateCount(cards.Count);
        AnimationManager.Instance.AddAnimation(Discard(card));
    }

    private void OnRemoveAnim(CardBase card)
    {
        UpdateCount(cards.Count);
    }
    private void UpdateCount(int value)
    {
        if (count) count.text = value.ToString();
    }
}
