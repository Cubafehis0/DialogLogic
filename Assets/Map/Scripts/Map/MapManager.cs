using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[Serializable]
public class StringSpriteDictionary : SerializableDictionary<String, Sprite> { }
public class MapManager : MonoBehaviour
{
    private Map map = null;

    private int currentIndex;

    [SerializeField]
    private StringSpriteDictionary placeSpriteDictionnary = new StringSpriteDictionary();

    [SerializeField]
    private Image placeImage;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text desText;

    private void Start()
    {
        map = GameManager.Instance.Map;
        currentIndex = 0;
    }

    public void OnClickLeftButton()
    {
        if (currentIndex == 0) currentIndex = map.placesDic.Count - 1;
        else currentIndex--;
        if (placeSpriteDictionnary.TryGetValue(map.placesDic[currentIndex].name, out Sprite sprite))
        {
            placeImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("没有该地图的图片");
        }
        nameText.text = map.placesDic[currentIndex].name;
    }

    public void OnClickRightButton()
    {
        if (currentIndex == map.placesDic.Count - 1) currentIndex = 0;
        else currentIndex++;
        if (placeSpriteDictionnary.TryGetValue(map.placesDic[currentIndex].name, out Sprite sprite))
        {
            placeImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("没有该地图的图片");
        }
        nameText.text = map.placesDic[currentIndex].name;
    }

    public void OnClickEnterPlace()
    {
        GameManager.Instance.EnterPlace(map.placesDic[currentIndex]);
    }
}
