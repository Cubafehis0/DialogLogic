using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MapScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer; 
    private static MapScript instance;
    public static MapScript Instance { get => instance; }
    private MapState mapState=MapState.MapDay;
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
        Debug.Log("UpdateMapState");
        Sprite sprite;
        if (mapSpriteDictionnary.TryGetValue(mapState, out sprite))
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
