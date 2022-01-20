using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardEventHandler
{
    void OnEventMouseEnter(Card sender);
    void OnEventMouseExit(Card card);
    void OnEventMousePress(Card sender, bool value);
}

public interface IPileListener
{
    void OnAdd(Card newCard);
    void OnRemove(Card oldCard);
    void OnShuffle();
}


[RequireComponent(typeof(Pile))]
public class HandPileObject : MonoBehaviour, ICardEventHandler, IPileListener
{

    /// <summary>
    /// ��ǰ����
    /// </summary>
    public static HandPileObject instance;


    //��������---------------------------------------------------------

    /// <summary>
    /// ������������ܳ��ȣ�ʵ�ʿ��ܻ�����ֵ�Դ�
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
    private float moveSpeed = 0.07f;

    //����������--------------------------------------------------


    /// <summary>
    /// ��ǰ�۽��Ŀ���
    /// </summary>
    [SerializeField]
    private Card focusedCard = null;
    private Card FocusedCard
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
                    CardObject cardObject = focusedCard.GetComponent<CardObject>();
                    if(cardObject) cardObject.OrderInLayer = cardObject.transform.GetSiblingIndex();
                }
                if (value && value.transform.IsChildOf(transform))
                {
                    value.transform.localScale = 1.5f * Vector3.one;
                    value.transform.localEulerAngles = Vector3.zero;
                    value.transform.position = cardsOffset[value.transform.GetSiblingIndex()];
                    value.transform.Translate(0.5f * Vector2.up);
                    CardObject cardObject = value.GetComponent<CardObject>();
                    if (cardObject) cardObject.OrderInLayer = 99;
                }
                focusedCard = value;
                RecalculatePosition();

            }
        }
    }




    // �м������,������Inspector���޸�-----------------------------------------------

    private float[] cardsArcOffset;

    private Vector3[] cardsOffset = new Vector3[0];

    [SerializeField]
    private bool lockFocus = false;

    private Pile pile = null;

    // �м��������-----------------------------------------------


    private void RecalculatePosition()
    {
        int count = pile.CardsList.Count;
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
            cardsOffset[i] += Vector3.forward * -0.1f * i;
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
        pile = GetComponent<Pile>();
    }

    private void OnEnable()
    {
        pile.UpdateBindingObject();
    }

    private void OnDisable()
    {
        pile.UpdateBindingObject();
    }

    private void Start()
    {
        NoTargetAimer.instance.AddCallback(delegate
        {
            lockFocus = false;
            FocusedCard = null;
        });
    }

    private void Update()
    {
        UpdatePosition();
    }



    private void UpdatePosition()
    {
        List<Card> cardsList = pile.CardsList;
        for (int i = 0; i < cardsList.Count; i++)
        {
            if (FocusedCard == null || cardsList[i] != FocusedCard)
            {
                Vector3 currentPos = cardsList[i].transform.position;
                Vector3 targetPos = cardsOffset[i];
                cardsList[i].transform.position = Vector3.MoveTowards(currentPos, targetPos, moveSpeed);
                cardsList[i].transform.localEulerAngles = -Mathf.Rad2Deg * (cardsArcOffset[i] / curvature) * Vector3.forward;
            }
        }
    }

    public void SelectCard(Card card)
    {
        FocusedCard = card;
        lockFocus = true;
        NoTargetAimer.instance.StartAiming(card);
    }

    public void OnEventMouseEnter(Card sender)
    {
        if (sender && !lockFocus) FocusedCard = sender;
    }

    public void OnEventMouseExit(Card card)
    {
        if (!lockFocus) FocusedCard = null;
    }


    public void OnEventMousePress(Card sender, bool value)
    {
        if (sender) SelectCard(sender);
    }

    public void OnAdd(Card card)
    {
        card.gameObject.SetActive(true);
        card.transform.SetParent(transform, true);
        card.transform.SetAsLastSibling();
        int siblintIndex = card.transform.GetSiblingIndex();
        CardObject cardObject = card.GetComponent<CardObject>();
        if (cardObject)
        {
            cardObject.OrderInLayer = siblintIndex;
            cardObject.EventHander = this;
        }
        RecalculatePosition();
    }

    public void OnRemove(Card oldCard)
    {
        if (oldCard == focusedCard)
        {
            lockFocus = false;
            focusedCard = null;
        }
        oldCard.transform.SetParent(null, true);
        RecalculatePosition();
    }

    public void OnShuffle()
    {
        return;
    }
}
