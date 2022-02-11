using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCardLibrary : CardLibrary
{
    private static CardLibrary instance = null;
    public static CardLibrary Instance
    {
        get => instance;
    }

    //static readonly string testHoldEffect = "lgcPlus";
    //static readonly string testHoldEffectScale = "1";
    //static readonly string testCondition = "NIL";
    //static readonly string testConditionScale = "0";
    //static readonly string testEffect = "lgcPlus";
    //static readonly string testEffectScale = "3";
    //static readonly string testPostEffect = "NIL";
    //static readonly string testPostEffectScale = "0";

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        //cards.Clear();
        //Card testCard1 = new Card(1001, testHoldEffect, testHoldEffectScale, testCondition, testConditionScale, testEffect, "1", testPostEffect, testPostEffectScale);
        //Card testCard2 = new Card(1002, testHoldEffect, testHoldEffectScale, testCondition, testConditionScale, testEffect, "2", testPostEffect, testPostEffectScale);
        //Card testCard3 = new Card(1003, testHoldEffect, testHoldEffectScale, testCondition, testConditionScale, testEffect, "3", testPostEffect, testPostEffectScale);
        //Card testCard4 = new Card(1004, testHoldEffect, testHoldEffectScale, testCondition, testConditionScale, testEffect, "4", testPostEffect, testPostEffectScale);
        //Card testCard5 = new Card(1005, testHoldEffect, testHoldEffectScale, testCondition, testConditionScale, testEffect, "5", testPostEffect, testPostEffectScale);
        //Card testCard6 = new Card(1006, testHoldEffect, testHoldEffectScale, testCondition, testConditionScale, testEffect, "6", testPostEffect, testPostEffectScale);
        //cards.Add(testCard1);
        //cards.Add(testCard2);
        //cards.Add(testCard3);
        //cards.Add(testCard4);
        //cards.Add(testCard5);
        //cards.Add(testCard6);
    }
}
