using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SemanticTree;
using System.IO;
using CardGame.Recorder;
using UnityEngine.UI;

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
    [SerializeField]
    public CardRecorder CardRecorder = new CardRecorder();
    public DragHandPileObject handPileObject;
    public Image enemyImage;

    private static CardGameManager instance = null;
    public static CardGameManager Instance
    {
        get => instance;
    }

    void Awake()
    {
        instance = this;
        ExpressionAnalyser.ExpressionParser.VariableTable = new Context();
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
        string name = GameManager.Instance.currentIncident.target;
        enemyImage.sprite = GameManager.Instance.EnemySpriteDictionary[name];
    }

    private IEnumerator TurnCoroutine()
    {
        int i = 0;
        for (i = 0; i < 100; i++)
        {
            if (dialogSystem.NextState == Ink2Unity.InkState.Finish) break;
            turn++;
            isPlayerTurn = false;
            Context.PushPlayerContext(enemy);
            Context.Target = playerState;
            enemyController.StartTurn();
            yield return new WaitUntil(() => enemyController.EndTurnTrigger);
            enemyController.EndTurn();
            Context.Target = null;
            Context.PopPlayerContext();

            do
            {
                if (dialogSystem.NextState == Ink2Unity.InkState.Finish) break;
                isPlayerTurn = true;
                Context.PushPlayerContext(playerState);
                Context.Target = enemy;
                playerController.StartTurn();
                yield return new WaitUntil(() => playerController.EndTurnTrigger);
                playerController.EndTurn();
                Context.Target = null;
                Context.PopPlayerContext();
            } while (((TurnControllerPlayerDialog)playerController).additionalTurn);

        }
        if (i == 100) Debug.LogWarning("回合数达到上限100");
        GUISystemManager.Instance.OpenSelectLootGUISystem(GetRandomLoots());
        yield return new WaitUntil(() => ForegoundGUISystem.current == null);
        GameManager.Instance.CompleteCurrentIncident();
    }

    public List<string> GetRandomLoots()
    {
        List<string> allCards = GameManager.Instance.CardLibrary.GetAllCards();
        MyMath.Shuffle(allCards);
        return allCards.GetRange(0, 3);
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


}
