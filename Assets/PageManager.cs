using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    private int pageIndex = 0;
    [SerializeField]
    private GameObject[] pages;
    public int PageIndex
    {
        get => pageIndex;
        set
        {
            pageIndex = Mathf.Clamp(value, 0, pages.Length - 1);
            UpdateVisuals();
        }
    }
    public void MoveNext()
    {
        PageIndex = (PageIndex + 1) % pages.Length;
    }

    public void Previous()
    {
        PageIndex = (PageIndex - 1 + pages.Length) % pages.Length;
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex);
        }
    }

    private void OnValidate()
    {
        PageIndex = pageIndex;
        UpdateVisuals();
    }
    private void Start()
    {
        UpdateVisuals();
    }
}
