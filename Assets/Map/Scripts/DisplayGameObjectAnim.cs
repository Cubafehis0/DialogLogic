using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGameObjectAnim : MonoBehaviour
{
    public float time = 1f;
    private Image[] images;
    private SpriteRenderer[] spriteRenderers;

    /// <summary>
    /// 渐隐效果,时长1秒
    /// </summary>
    /// <param name="gameObject"></param>
    public void DisPlayGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
        images = gameObject.GetComponentsInChildren<Image>();
        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer.color.a > 0)
                spriteRenderer.color -= new Color(0, 0, 0, 1f);
            Debug.Log(spriteRenderer.color);
            yield return new WaitForSeconds(time);
        }
        foreach (Image image in images)
        {
            if (image.color.a > 0)
                image.color -= new Color(0, 0, 0, 1f);
            yield return new WaitForSeconds(time);

        }
        Debug.Log("1");
    }
}


