using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ink.Runtime;

public interface IRichButton
{
    void SetSprite(Sprite sprite, Sprite[] sprites = null);
    void SetText(string Button_text);
}

public class RichButton : MonoBehaviour, IRichButton
{
    [HideInInspector]
    public Button btn;
    [SerializeField]
    private Image[] imagesOfLogo;
    protected Image img;
    protected Text txt;
    public UnityEvent<RichButton> OnClick = new UnityEvent<RichButton>();

    private void Awake()
    {
        btn = GetComponent<Button>();
        txt = GetComponentInChildren<Text>();
        img = GetComponent<Image>();
    }

    protected virtual void Start()
    {
        ClearLogoImages();
        btn.onClick.AddListener(delegate { OnClick.Invoke(this); });
    }

    public void SetText(string Button_text)
    {
        if (txt != null)
        {
            txt.text = Button_text;
        }
        else
        {
            Debug.LogWarning(name + " 缺少Text组件");
        }
    }

    public void SetSprite(Sprite sprite, Sprite[] sprites = null)
    {
        ClearLogoImages();
        if (img != null)
        {
            img.sprite = sprite;
        }
        else
        {
            Debug.LogWarning(name + " 缺少Image组件");
        }
        if (sprites != null) 
        {
            for(int i = 0; i < sprites.Length; i++) 
            {
                if (imagesOfLogo.Length > i)
                {
                    imagesOfLogo[i].sprite = sprites[i];
                    imagesOfLogo[i].color = new Color(imagesOfLogo[i].color.r, imagesOfLogo[i].color.g, imagesOfLogo[i].color.b, 255f);
                }
                else
                {
                    Debug.LogWarning("imagesOfLogo数量不够");
                }
            }
        }
    }
    private void ClearLogoImages() 
    {
        foreach(Image img in imagesOfLogo)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
    }
}
