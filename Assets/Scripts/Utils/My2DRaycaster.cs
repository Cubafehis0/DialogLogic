using UnityEngine;

/// <summary>
/// 工具类
/// ·获得当前鼠标下方物体(必须有Collider)
/// </summary>
public class My2DRaycaster : MonoBehaviour
{
    /// <summary>
    /// 获得当前鼠标下方物体(必须有Collider)
    /// </summary>
    /// <param name="bitMask">layermask</param>
    /// <returns>鼠标下方物体</returns>
    public static GameObject GetCurrentObject(int bitMask)
    {
        Vector2 start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = Vector2.zero;

        RaycastHit2D[] hit = Physics2D.RaycastAll(start, dir, 10f, bitMask);

        if (hit.Length > 0 && hit[0].collider != null)
        {
            return hit[0].collider.gameObject;

        }

        return null;
    }


    /// <summary>
    /// 获得当前鼠标下方物体(必须有Collider)
    /// </summary>
    /// <returns>鼠标下方物体</returns>
    public static GameObject GetCurrentObject()
    {
        Vector2 start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = Vector2.zero;


        RaycastHit2D hit = Physics2D.Raycast(start, dir);

        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
