using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ӧ����¼��ӿ�
public interface ICardEventHandler
{
    void OnEventMouseEnter(Object sender);
    void OnEventMouseExit();
    void OnEventMousePress(Object sender, bool value);
}

// CardObject��Ӧ��ʵ���ڵ�GameObject
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class CardObject : MonoBehaviour
{
    [HideInInspector]
    public ICardEventHandler eventHandler = null;

    [HideInInspector]
    public Collider2D volume = null;

    [HideInInspector]
    public Renderer spriteRenderer = null;

    public Deck deck = null;

    /// <summary>
    /// δ���
    /// ����������������ĳ�Card��Ϣ��
    /// </summary>
    public string desc = "";

    public bool needTarget = false;

    
    private void Awake()
    {
        volume = GetComponentInChildren<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }



    private void OnMouseEnter()
    {
        if (eventHandler != null) eventHandler.OnEventMouseEnter(this);
    }

    private void OnMouseExit()
    {
        if (eventHandler != null) eventHandler.OnEventMouseExit();
    }

    private void OnMouseDown()
    {
        if (eventHandler != null) eventHandler.OnEventMousePress(this, true);
    }

}
