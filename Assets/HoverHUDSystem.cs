using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverHUDSystem : MonoBehaviour
{
    private static HoverHUDSystem instance;
    public static HoverHUDSystem Instance { get { return instance; } }

    [SerializeField]
    private GameObject label;
    private void Awake()
    {
        instance = this;
        label.SetActive(false);
    }

    public void OpenConditionHUD(string msg)
    {
        gameObject.SetActive(true);
        label.SetActive(true);
        label.GetComponentInChildren<Text>().text= msg;
        label.transform.position = Input.mousePosition;
        label.transform.Translate(new Vector3(100,100,0));
    }

    public void CloseConditionHUD()
    {
        label.SetActive(false);
    }
}
