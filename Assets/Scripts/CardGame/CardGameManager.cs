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

    public CardPlayerState playerState;
    public CardPlayerState enemy;
    public bool isPlayerTurn = false;


    private static CardGameManager instance = null;
    public static CardGameManager Instance
    {
        get => instance;
    }

    private void Start()
    {
        if (GameManager.Instance.currentStory == null)
        {
            Debug.LogError("没有设定当前故事");
            return;
        }


        turn = 0;
        DialogSystem.Instance.Open(GetInkStoryAsset(GameManager.Instance.currentStory));
        playerState.Init(GameManager.Instance.localPlayer);
    }

    public void SetGameConfig(GameConfig config)
    {
        playerState.Player.PlayerInfo = config.PlayerInfo;
        //enemy.Player.PlayerInfo.Personality = config.enemyPersonality;
    }

    private TextAsset GetInkStoryAsset(string name)
    {
        string InkStoryPath = Path.Combine(Application.streamingAssetsPath, "InkStories");
        if (!Directory.Exists(InkStoryPath))
        {
            Debug.LogError("Asset目录下不存在InkStory文件");
            return null;
        }
        string[] filePath = Directory.GetFiles(InkStoryPath, name + ".json", SearchOption.AllDirectories);
        if (filePath.Length <= 0)
        {
            Debug.LogError($"InkStory目录下不存在名为{name}的文件");
            return null;
        }
        return new TextAsset(File.ReadAllText(filePath[0]));
    }


    public int Turn { get => turn; }

    public Card GetCardCopy(Card prefab)
    {
        Card ret = Instantiate(prefab.gameObject).GetComponent<Card>();
        ret.Construct(prefab);
        ret.player = playerState;
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
        if (playerState.CanChoose(slot))
        {
            isPlayerTurn = false;
            DialogSystem.Instance.ForceSelectChoice(slot.Choice, playerState.JudgeChooseSuccess(slot));

        }
    }
    /// <summary>
    /// 结束当前回合
    /// </summary>
    public void EndTurn()
    {
        playerState.EndTurn();
    }

    /// <summary>
    /// 开启一个回合
    /// </summary>
    public void StartTurn()
    {
        turn++;
        isPlayerTurn = true;
        playerState.StartTurn();

    }
}
