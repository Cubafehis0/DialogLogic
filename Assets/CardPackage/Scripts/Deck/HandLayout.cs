using UnityEngine;
using UnityEngine.EventSystems;

public class HandLayout : MonoBehaviour
{

    /// <summary>
    /// 当空间充足时，相邻卡牌中心点的距离
    /// 达到maxLength后，间隔会被压缩
    /// </summary>
    [SerializeField]
    private float spacing = 200f;

    /// <summary>
    /// 曲率半径
    /// </summary>
    [SerializeField]
    private float curvature = 2000f;

    /// <summary>
    /// 正态方差
    /// 根据3sigma原则，查看手牌时，大于2*variance的卡牌不会改变位置
    /// </summary>
    [SerializeField]
    private float variance = 2f;

    /// <summary>
    /// 卡牌在手牌动画中的移动速度
    /// </summary>
    [SerializeField]
    private float moveSpeed = 10f;


    private bool lockTransform = false;
    private bool lockFocus = false;
    private float maxLength;
    private float[] cardsArcOffset;
    private Vector3[] cardsOffset = new Vector3[0];
    [SerializeField]
    private Transform focusedCard = null;
    public Transform FocusedCard
    {
        get => focusedCard;
        set
        {
            if (LockFocus) return;
            if (value != focusedCard)
            {
                if (focusedCard)
                {
                    focusedCard.transform.localScale = Vector3.one;
                    CardObjectBase cardObject = focusedCard.GetComponent<CardObjectBase>();
                    if (cardObject) cardObject.OrderInLayer = 0;
                    //lockTransform = false;
                    //focusedCard.transform.SetParent(transform, true);
                }
                if (value && value.transform.IsChildOf(transform))
                {
                    value.transform.localScale = 1.5f * Vector3.one;
                    value.transform.localEulerAngles = Vector3.zero;
                    value.transform.position = cardsOffset[value.transform.GetSiblingIndex()];
                    value.transform.Translate(0.5f * Vector2.up);
                    CardObjectBase cardObject = value.GetComponent<CardObjectBase>();
                    if (cardObject) cardObject.OrderInLayer = 99;
                }
                focusedCard = value;
                RecalculatePosition();
            }
        }
    }

    public bool LockFocus { get => lockFocus; set => lockFocus = value; }

    protected void RecalculatePosition()
    {
        Rect rect = GetComponent<RectTransform>().rect;
        maxLength = rect.width;
        int count = transform.childCount;
        if (count == 0) return;
        if (count == 1)
        {
            cardsArcOffset = new float[1] { 0 };
            cardsOffset = new Vector3[1] { transform.position };
            return;
        }
        cardsArcOffset = new float[count];
        cardsOffset = new Vector3[count];
        float totalLength = Mathf.Min(maxLength, spacing * (count - 1));
        if (FocusedCard)
        {
            int focusedIndex = FocusedCard.transform.GetSiblingIndex();
            for (int i = 0; i < count; i++)
            {
                if (i > focusedIndex) cardsArcOffset[i] += 1f * MyMath.GetProbability(i - 1, i, focusedIndex, variance);
                if (i < focusedIndex) cardsArcOffset[i] -= 1f * MyMath.GetProbability(i, i + 1, focusedIndex, variance);
            }
        }
        Vector2 pos = transform.position;
        for (int i = 0; i < count; i++)
        {
            float ratio = 1f * i / (count - 1);
            cardsArcOffset[i] += totalLength * (ratio - 0.5f);
            cardsOffset[i] = pos + MyMath.CalcCircleOffset(cardsArcOffset[i] / curvature, curvature);
            cardsOffset[i] += -0.1f * i * Vector3.forward;
        }
    }

    private void OnTransformChildrenChanged()
    {
        if (lockTransform) return;
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        RecalculatePosition();
        lockFocus = false;
        FocusedCard = null;
    }
    private void OnEnable()
    {
        RecalculatePosition();
    }

    private void Update()
    {
        RecalculatePosition();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (FocusedCard == null || child != FocusedCard)
            {
                Vector3 currentPos = child.position;
                Vector3 targetPos = cardsOffset[i];
                child.position = Vector3.MoveTowards(currentPos, targetPos, Time.deltaTime * moveSpeed);
                child.localEulerAngles = -Mathf.Rad2Deg * (cardsArcOffset[i] / curvature) * Vector3.forward;
            }
        }
    }



    public void Focus(BaseEventData eventData)
    {
        FocusedCard = ((PointerEventData)eventData).pointerEnter.GetComponentInParent<CardObjectBase>().transform;
    }

    public void ResetFocus(BaseEventData eventData)
    {
        FocusedCard = null;
    }
}