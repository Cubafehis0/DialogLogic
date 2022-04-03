using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class MapScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mapSpriteRenderer;
    [SerializeField]
    private Image gearImage;

    private static MapScript instance;
    public static MapScript Instance { get => instance; }
    private MapState mapState = MapState.MapDay;
    private UnityEvent OnChooseState = new UnityEvent();
    public MapState MapState
    {
        get => mapState;
        set
        {
            mapState = value;
            OnChooseState.Invoke();
        }
    }
    private Dictionary<MapState, Sprite> mapSpriteDictionnary = new Dictionary<MapState, Sprite>();
    [SerializeField]
    private Sprite[] mapSprites;
    [SerializeField]
    private Sprite[] gearSprites;
    private void Awake()
    {
        instance = this;
        OnChooseState.AddListener(UpdateMapState);
        if (mapSpriteDictionnary.Count != 8)
        {
            mapSpriteDictionnary.Clear();
            for (int i = 0; i < 8; i++)
                mapSpriteDictionnary.Add((MapState)i, mapSprites[i]);
        }
    }
    private void UpdateMapState()
    {
        //Debug.Log("UpdateMapState");
        Sprite sprite;
        if (mapSpriteDictionnary.TryGetValue(mapState, out sprite))
        {
            mapSpriteRenderer.sprite = sprite;
            if ((int)mapState < 4)
            {
                gearImage.sprite = gearSprites[0];
            }
            else
            {
                gearImage.sprite = gearSprites[1];
            }
        }
    }
}
