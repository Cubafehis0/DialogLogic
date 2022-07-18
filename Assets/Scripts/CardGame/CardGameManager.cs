using CardGame.Recorder;
using JasperMod.SemanticTree;
using ModdingAPI;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{


    public CardPlayerState playerState;
    public CardPlayerState enemy;

    [SerializeField]
    private TurnManager turnManager;

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
    }

    private void Start()
    {
        if (GameManager.Instance.CurrentStory == null)
        {
            Debug.LogError("û���趨��ǰ����");
            return;
        }
   
        dialogSystem.Open(GetInkStoryAsset(GameManager.Instance.CurrentStory));
        playerState.Init(GameManager.Instance.LocalPlayer);
        string name = GameManager.Instance.currentIncident.target;
        enemyImage.sprite = AssetsManager.Instance.enemySpriteDictionary[name];
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

    public TurnManager TurnManager { get => turnManager;}
}
