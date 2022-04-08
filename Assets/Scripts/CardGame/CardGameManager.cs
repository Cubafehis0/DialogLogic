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
    public bool isPlayerTurn=false;


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
            Debug.LogError("AssetĿ¼�²�����InkStory�ļ�");
            return;
        }
        string[] filePath = Directory.GetFiles(InkStoryPath, name + ".json", SearchOption.AllDirectories);
        if (filePath.Length <= 0)
        {
            Debug.LogError($"InkStoryĿ¼�²�������Ϊ{name}���ļ�");
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
            isPlayerTurn = false;
            DialogSystem.Instance.ForceSelectChoice(slot.Choice, player.JudgeChooseSuccess(slot));
        
        }
    }

    /// <summary>
    /// ����һ����Ϸ
    /// </summary>
    public void StartGame()
    {
        turn = 0;
        player.Init();
    }
    /// <summary>
    /// ������ǰ�غ�
    /// </summary>
    public void EndTurn()
    {
        player.EndTurn();
    }

    /// <summary>
    /// ����һ���غ�
    /// </summary>
    public void StartTurn()
    {
        turn++;
        isPlayerTurn = true;
        player.StartTurn();

    }
}
