using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoleImageControl : MonoBehaviour
{
    [System.Serializable]
    public class _Sprites
    {
        public string name;
        public Sprite sprite;
    }
    [SerializeField]
    _Sprites[] _sprites;
    public Dictionary<string, Sprite> sprites=new Dictionary<string, Sprite>();
    public Sprite defaultSprite;
    static RoleImageControl instance;
    public static RoleImageControl GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
        foreach (var _sprite in _sprites)
        {
            sprites.Add(_sprite.name, _sprite.sprite);
        }
    }
    public Sprite GetSpriteByName(string name)
    {
        if(sprites.ContainsKey(name))
        {
            return sprites[name];
        }
        return null;   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
