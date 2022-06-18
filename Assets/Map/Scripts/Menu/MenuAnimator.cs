using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MapStateSpriteDictionary : SerializableDictionary<MenuState, Sprite> { }
public class MenuAnimator : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mapSpriteRenderer;
    [SerializeField]
    private Image gearImage;
    [SerializeField]
    private MapStateSpriteDictionary dayMapSpriteDictionnary = new MapStateSpriteDictionary();
    [SerializeField]
    private MapStateSpriteDictionary nightMapSpriteDictionnary = new MapStateSpriteDictionary();
    [SerializeField]
    private Sprite[] gearSprites;

    private bool isChoose = false;
    private bool isInventor = false;
    private bool isRest = false;
    private bool isDay = true;


    private static MenuAnimator instance;
    public static MenuAnimator Instance { get => instance; }

    public bool IsRest
    {
        get => isRest;
        set
        {
            isRest = value;
            UpdateMapState();
        }
    }
    public bool IsChoose
    {
        get => isChoose;
        set
        {
            isChoose = value;
            UpdateMapState();
        }
    }
    public bool IsInventor
    {
        get => isInventor;
        set
        {
            isInventor = value;
            UpdateMapState();
        }
    }

    public bool IsDay
    {
        get => isDay;
        set
        {
            isDay = value;
            UpdateMapState();
        }
    }

    public void SwitchDayNight()
    {
        isDay = !isDay;
        UpdateMapState();
    }

    private void Awake()
    {
        instance = this;
    }
    private void UpdateMapState()
    {
        MenuState mapState = MenuState.Default;
        if (isChoose) mapState = MenuState.Choose;
        if (isRest) mapState = MenuState.Rest;
        if (isInventor) mapState = MenuState.Holdings;
        var d = IsDay ? dayMapSpriteDictionnary : nightMapSpriteDictionnary;
        if (d.TryGetValue(mapState, out Sprite sprite))
        {
            mapSpriteRenderer.sprite = sprite;
        }
        gearImage.sprite = IsDay ? gearSprites[0] : gearSprites[1];
    }
}
