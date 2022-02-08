using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DisplayInformationAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private string message;
    public string Message{ get => message; set => message = value; }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.gameObject.SetActive(false);
    }
    private void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
        text.gameObject.SetActive(false);
    }
}
