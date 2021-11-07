using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameLoop : MonoBehaviour
{
    UIController m_UIController = new UIController();

    public void Start()
    {
        DontDestroyOnLoad(this);
        m_UIController.Start();
    }

    public void Update()
    {
        //Debug.Log("update");
        m_UIController.Update();
        GameObject gameObject = EventSystem.current.currentSelectedGameObject;
        if (gameObject != null) Debug.Log(gameObject.name);

        //if (Input.GetMouseButtonDown(0))
        //{

        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        Debug.Log("�����UI");
        //    }
        //    else
        //    {
        //        Debug.Log("û�����UI");

        //    }
        //}

        var mouse = Mouse.current;
        if (mouse == null)
        {
            //Debug.Log("null");
            return;
        }

        if (mouse.leftButton.wasPressedThisFrame)
        {
            //Debug.Log("pressed left button");
            var onScreenPosition = mouse.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(onScreenPosition);

            var hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.transform.position = ray.origin;
            }
            else Debug.Log("hit.collider = null");
        }


    }

    /// <summary>
    /// ����Ƿ���UI
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <returns></returns>
    //private bool IsPointerOverGameObject(Vector2 mousePosition)
    //{
    //    //����һ������¼�
    //    PointerEventData eventData = new PointerEventData(EventSystem.current);
    //    eventData.position = mousePosition;
    //    List<RaycastResult> raycastResults = new List<RaycastResult>();
    //    //����λ�÷���һ�����ߣ�����Ƿ���UI
    //    EventSystem.current.RaycastAll(eventData, raycastResults);
    //    //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    //    if (raycastResults.Count > 0)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}