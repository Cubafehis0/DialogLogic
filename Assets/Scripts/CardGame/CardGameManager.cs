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
            Debug.LogError("û���趨��ǰ����");
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
    /// ������ǰ�غ�
    /// </summary>
    public void EndTurn()
    {
        isPlayerTurn = false;
        playerState.EndTurn();
    }

    /// <summary>
    /// ����һ���غ�
    /// </summary>
    public void StartTurn()
    {
        turn++;
        isPlayerTurn = true;
        playerState.StartTurn();

    }
}
