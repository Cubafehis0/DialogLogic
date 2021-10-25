using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardDescHUD : MonoBehaviour
{
    public static CardDescHUD instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this);
            Debug.LogError("场景中存在多个CardDescHUD单例");
            return;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    [SerializeField]
    private Text text;
    public void CreateCardDesc(string content, Vector2 worldPosition)
    {
        Debug.Log(worldPosition);
        gameObject.SetActive(true);
        text.text = content;
        Vector2 pos= Camera.main.WorldToScreenPoint(worldPosition);
        gameObject.transform.position = pos;
    }

    public void DestroyCardDesc()
    {
        gameObject.SetActive(false);
    }
}
