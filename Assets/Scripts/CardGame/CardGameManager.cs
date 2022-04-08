using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;
using System.IO;

public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private int turn = 0;

    public CardPlayerState player;
    public CardPlayerState enemy;


    private static CardGameManager instance = null;
    public static CardGameManager Instance
    {
        get => instance;
    }

    public void SetGameConfig(GameConfig config)
    {
        SetInkStoryAsset(config.StoryName);
        player.Player.PlayerInfo = config.PlayerInfo;
        //enemy.Player.PlayerInfo.Personality = config.enemyPersonality;
    }

    private void SetInkStoryAsset(string name)
    {
        string InkStoryPath = Path.Combine(Application.streamingAssetsPath, "InkStories");
        if (!Directory.Exists(InkStoryPath))
        {
            Debug.LogError("Asset目录下不存在InkStory文件");
            return;
        }
        string[] filePath = Directory.GetFiles(InkStoryPath, name + ".json", SearchOption.AllDirectories);
        if (filePath.Length <= 0)
        {
            Debug.LogError($"InkStory目录下不存在名为{name}的文件");
            return;
        }
        DialogSystem.Instance.Open(new TextAsset(File.ReadAllText(filePath[0])));
    }


    public int Turn { get => turn; }

    public Card GetCardCopy(Card prefab)
    {
        Card ret = Instantiate(prefab.gameObject).GetComponent<Card>();
        ret.Construct(prefab);
        ret.player = player;
        return ret;
    }

    public void ReturnCardObject(CardObject cardObject)
    {
        Destroy(cardObject.gameObject);
    }

    void Awake()
    {
        instance = this;
        ExpressionAnalyser.ExpressionParser.VariableTable = new Context();
    }

    public void SlotSelectCallback(ChoiceSlot slot)
    {
        if (player.CanChoose(slot))
        {
            DialogSystem.Instance.ForceSelectChoice(slot.Choice, player.JudgeChooseSuccess(slot));
        }
    }

    /// <summary>
    /// 开启一局游戏
    /// </summary>
    public void StartGame()
    {
        turn = 0;
        player.Init();
    }
    /// <summary>
    /// 结束当前回合
    /// </summary>
    public void EndTurn()
    {
        player.EndTurn();
    }

    /// <summary>
    /// 开启一个回合
    /// </summary>
    public void StartTurn()
    {
        turn++;
        player.StartTurn();
    }
}
