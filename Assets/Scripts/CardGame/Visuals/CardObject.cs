using UnityEngine;
using UnityEngine.UI;

public class CardObject : MonoBehaviour
{
    [SerializeField]
    private Card card = null;
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text cdtDescText;
    [SerializeField]
    private Text eftDescText;
    [SerializeField]
    private Text memeText;
    private Canvas cardUICanvas = null;
    private int orderInLayer = 0;
    [SerializeField]
    private bool draggable = false;
    public bool Draggable
    {
        get => draggable;
        set
        {
            draggable = value;
            var c = GetComponent<Draggable>();
            if (c == null) c = gameObject.AddComponent<Draggable>();
            c.enabled = value;
        }
    }


    private void Awake()
    {
        cardUICanvas = GetComponent<Canvas>();
    }

    public int OrderInLayer
    {
        get => orderInLayer;
        set
        {
            cardUICanvas.overrideSorting = value > 0;
            cardUICanvas.sortingOrder = value;
            orderInLayer = value;
        }
    }

    public Card Card
    {
        get => card;
        set
        {
            card = value;
            UpdateVisuals();
        }
    }

    public void UpdateVisuals()
    {
        if (card == null) return;
        if (titleText) titleText.text = card.info.Title;
        if (cdtDescText) cdtDescText.text = card.info.ConditionDesc;
        if (eftDescText) eftDescText.text = card.info.EffectDesc;
        if (memeText) memeText.text = card.info.Meme;
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }
}
