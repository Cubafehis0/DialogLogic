using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Deck))]
public class HandDeckArrangement : MonoBehaviour, ICardEventHandler, IDeckEventHandler
{

    /// <summary>
    /// ��ǰ����
    /// </summary>
    public static HandDeckArrangement instance;


    //��������---------------------------------------------------------

    /// <summary>
    /// ������������ܳ��ȣ�ʵ�ʿ��ܻ�����ֵ�Դ�
    /// ���ǣ�OnGUI()��ʾ
    /// </summary>
    [SerializeField]
    private float maxLength = 10f;

    /// <summary>
    /// ���ռ����ʱ�����ڿ������ĵ�ľ���
    /// �ﵽmaxLength�󣬼���ᱻѹ��
    /// </summary>
    [SerializeField]
    private float spacing = 1f;

    /// <summary>
    /// ���ʰ뾶
    /// </summary>
    [SerializeField]
    private float curvature = 10f;

    /// <summary>
    /// ��̬����
    /// ����3sigmaԭ�򣬲鿴����ʱ������2*variance�Ŀ��Ʋ���ı�λ��
    /// </summary>
    [SerializeField]
    private float variance = 2f;

    /// <summary>
    /// ���������ƶ����е��ƶ��ٶ�
    /// </summary>
    [SerializeField]
    private float moveSpeed = 0.01f;

    //����������--------------------------------------------------


    /// <summary>
    /// ��ǰ�۽��Ŀ���
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




    // �м������,������Inspector���޸�-----------------------------------------------

    private float[] cardsArcOffset;

    private Vector2[] cardsOffset;

    [SerializeField]
    private bool lockFocus = false;

    private Deck deck = null;

    // �м��������-----------------------------------------------


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
            Debug.LogError("��������HandsArrangement����");
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
