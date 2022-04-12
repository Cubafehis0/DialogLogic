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
    [SerializeField]
    private TurnController playerController;
    [SerializeField]
    private TurnController enemyController;


    public CardPlayerState playerState;
    public CardPlayerState enemy;
    public bool isPlayerTurn = false;
    [SerializeField]
    public DialogSystem dialogSystem;

    private static CardGameManager instance = null;
    public static CardGameManager Instance
    {
        get => instance;
    }

    private void Start()
    {
        if (GameManager.Instance.CurrentStory == null)
        {
            Debug.LogError("没有设定当前故事");
            return;
        }
        turn = 0;
        dialogSystem.Open(GetInkStoryAsset(GameManager.Instance.CurrentStory));
        StartCoroutine(TurnCoroutine());
        playerState.Init(GameManager.Instance.LocalPlayer);
    }

    private IEnumerator TurnCoroutine()
    {
        enemyController.StartTurn();
        for (int i = 0; i < 100; i++)
        {
            if (dialogSystem.NextState == Ink2Unity.InkState.Finish) yield break;
            turn++;
            isPlayerTurn = false;
            enemyController.StartTurn();
            yield return new WaitUntil(() => enemyController.EndTurnTrigger);
            if (dialogSystem.NextState == Ink2Unity.InkState.Finish) yield break;
            isPlayerTurn = true;
            playerController.StartTurn();
            yield return new WaitUntil(() => playerController.EndTurnTrigger);
        }
        Debug.LogWarning("回合数达到上限100");
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
    public void ReturnCardObject(CardObject cardObject)
    {
        Destroy(cardObject.gameObject);
    }

    void Awake()
    {
        instance = this;
        ExpressionAnalyser.ExpressionParser.VariableTable = new Context();
    }
}
