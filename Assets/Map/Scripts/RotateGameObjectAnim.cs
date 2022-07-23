using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class RotateGameObjectAnim : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private bool rotateWithTime = false;
    [SerializeField]
    private bool canRotateByMouse = false;
    [SerializeField]
    float x, y = 0;

    Quaternion v;
    float time = 0;
    public void OnDrag(PointerEventData eventData)
    {
        if (canRotateByMouse)
        {
            //Debug.Log("OnMouseDrag");
            x += Input.GetAxis("Mouse X");
            y += Input.GetAxis("Mouse Y");
            v = Quaternion.Euler(0, 0, (x - y) * 3);
            transform.rotation = v;
            //if (gameObject.layer == LayerMask.NameToLayer("UI")) Debug.Log("1");
        }
    }

    private void Update()
    {
        if (rotateWithTime)
        {
            time += Time.deltaTime;
            v = Quaternion.Euler(0, 0, time * 10);
            transform.rotation = v;
        }

    }

    public void OnMouseDrag()
    {
        if (canRotateByMouse)
        {
            //Debug.Log("OnMouseDrag");
            x += Input.GetAxis("Mouse X");
            y += Input.GetAxis("Mouse Y");
            v = Quaternion.Euler(0, 0, (x - y) * 3);
            transform.rotation = v;
        }
    }
}
