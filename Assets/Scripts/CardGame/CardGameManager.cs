using CardGame.Recorder;
using JasperMod.SemanticTree;
using System.Collections;
using System.IO;
using UnityEngine;
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
            Debug.LogError("û���趨��ǰ����");
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
            if (dialogSystem.InkStory.NextState == Ink2Unity.InkState.Finish) break;
            turn++;
            isPlayerTurn = false;
            enemyController.StartTurn();
            yield return new WaitUntil(() => enemyController.EndTurnTrigger);
            enemyController.EndTurn();
            do
            {
                if (dialogSystem.InkStory.NextState == Ink2Unity.InkState.Finish) break;
                isPlayerTurn = true;
                playerController.StartTurn();
                yield return new WaitUntil(() => playerController.EndTurnTrigger);
                playerController.EndTurn();
            } while (playerController.AdditionalTurn);

        }
        if (i == 100) Debug.LogWarning("�غ����ﵽ����100");
        var loot = GameManager.Instance.CardLibrary.GetRandom(3);
        GUISystemManager.Instance.Open("w_select_loot", loot);
        yield return new WaitUntil(() => ForegoundGUISystem.current == null);
        GameManager.Instance.CompleteCurrentIncident();
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
            Debug.LogError("AssetĿ¼�²�����InkStory�ļ�");
            return null;
        }
        string[] filePath = Directory.GetFiles(InkStoryPath, name + ".json", SearchOption.AllDirectories);
        if (filePath.Length <= 0)
        {
            Debug.LogError($"InkStoryĿ¼�²�������Ϊ{name}���ļ�");
            return null;
        }
        return new TextAsset(File.ReadAllText(filePath[0]));
    }


    public int Turn { get => turn; }


}
