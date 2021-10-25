using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Deck))]
public class HandDeckArrangement : MonoBehaviour, ICardEventHandler, IDeckEventHandler
{

    /// <summary>
    /// 当前单例
    /// </summary>
    public static HandDeckArrangement instance;


    //参数区：---------------------------------------------------------

    /// <summary>
    /// 所有手牌最大总长度，实际可能会比这个值稍大
    /// 卫星：OnGUI()显示
    /// </summary>
    [SerializeField]
    private float maxLength = 10f;

    /// <summary>
    /// 当空间充足时，相邻卡牌中心点的距离
    /// 达到maxLength后，间隔会被压缩
    /// </summary>
    [SerializeField]
    private float spacing = 1f;

    /// <summary>
    /// 曲率半径
    /// </summary>
    [SerializeField]
    private float curvature = 10f;

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
    private float moveSpeed = 0.01f;

    //参数区结束--------------------------------------------------


    /// <summary>
    /// 当前聚焦的卡牌
    /// </summary>
    [SerializeField]
    private CardObject focusedCard = null;
    private CardObject FocusedCard
    {
        get => focusedCard;
        set
        {
            if (lockFocus) return;
            if (value != focusedCard)
            {
                if (focusedCard)
                {
                    focusedCard.transform.localScale = Vector3.one;
                    focusedCard.spriteRenderer.sortingLayerName = "Default";
                }
                if (value && value.transform.IsChildOf(transform))
                {
                    value.transform.localScale = 1.5f * Vector3.one;
                    value.transform.localEulerAngles = Vector3.zero;
                    value.transform.position = cardsOffset[value.transform.GetSiblingIndex()];
                    value.transform.Translate(0.5f * Vector2.up);
                    value.spriteRenderer.sortingLayerName = "Focused";
                }
                focusedCard = value;
                RecalculatePosition();

            }
        }
    }




    // 中间变量区,无需在Inspector中修改-----------------------------------------------

    private float[] cardsArcOffset;

    private Vector2[] cardsOffset;

    [SerializeField]
    private bool lockFocus = false;

    private Deck deck = null;

    // 中间变量结束-----------------------------------------------


    private void RecalculatePosition()
    {
        CardObject[] cardsList = deck.CardsList;
        int count = cardsList.Length;
        if (count == 0) return;
        if (count == 1)
        {
            cardsArcOffset = new float[1] { 0 };
            cardsOffset = new Vector2[1] { transform.position };
            return;
        }
        cardsArcOffset = new float[count];
        cardsOffset = new Vector2[count];
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
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("不允许多个HandsArrangement单例");
        }
        deck = GetComponent<Deck>();
    }

    private void Start()
    {
        OneTargetAimer.instance.AddCallback(AimCallback);
        NoTargetAimer.instance.AddCallback(AimCallback);
        deck.UpdateReference();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        CardObject[] cardsList = deck.CardsList;
        for (int i = 0; i < cardsList.Length; i++)
        {
            if (cardsList[i] != FocusedCard)
            {
                Vector2 currentPos = cardsList[i].transform.position;
                Vector2 targetPos = cardsOffset[i];
                cardsList[i].transform.position = Vector2.MoveTowards(currentPos, targetPos, moveSpeed);
                cardsList[i].transform.localEulerAngles = -Mathf.Rad2Deg * (cardsArcOffset[i] / curvature) * Vector3.forward;
            }
        }
    }

    private void AimCallback(CardObject from, GameObject target)
    {
        lockFocus = false;
        FocusedCard = null;
    }

    public void SelectCard(CardObject card)
    {
        FocusedCard = card;
        lockFocus = true;
        if (card.needTarget)
        {
            OneTargetAimer.instance.StartAiming(card);
        }
        else
        {
            NoTargetAimer.instance.StartAiming(card);
        }
    }

    public void OnEventMouseEnter(Object sender)
    {
        CardObject card = sender as CardObject;
        if (card && !lockFocus) FocusedCard = card;
    }

    public void OnEventMouseExit()
    {
        if (!lockFocus) FocusedCard = null;
    }


    public void OnEventMousePress(Object sender, bool value)
    {
        CardObject card = sender as CardObject;
        if (card) SelectCard(card);
    }

    public void OnDeckUpdate()
    {
        RecalculatePosition();
    }

    public void OnDeckAdd(CardObject card)
    {
        card.spriteRenderer.sortingOrder = card.transform.GetSiblingIndex();
    }
}
