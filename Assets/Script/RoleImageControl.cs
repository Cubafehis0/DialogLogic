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
    static RoleImageControl instance;
    public static RoleImageControl GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(var _sprite in _sprites)
        {
            sprites.Add(_sprite.name, _sprite.sprite);
        }
    }
    public Sprite GetSpriteByName(string name)
    {
        Sprite result;
        if (sprites.TryGetValue(name, out result))
            return result;
        else
            return null;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
