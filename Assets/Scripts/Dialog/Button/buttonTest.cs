using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonTest : MonoBehaviour
{
    public Button btn;
    void Start()
    {
        int x = 1;
        btn.onClick.AddListener(delegate { OnClick(x); });
        btn.onClick.RemoveAllListeners();
    }

    private void OnClick(int i)
    {
        Debug.Log("click btn"+i);
    }
}
