using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    [SerializeField]
    private int turn = 0;
    [SerializeField]
    private int startIndex = 0;

    private bool isLooping = false;
    [SerializeField]
    private int index = 0;
    [SerializeField]
    private TurnController[] turnControllers = new TurnController[10];

    [SerializeField]
    private bool isPlayerTurn = false;
    [SerializeField]
    private TurnController turnControllerPrefab;

    public int Turn { get => turn; }
    public bool IsPlayerTurn { get => isPlayerTurn; }

    public void RegisterController(GameObject controller, int position)
    {
        if (0 <= position && position < 10)
        {
            if (turnControllers[position] == null)
            {
                turnControllers[position] = Instantiate(turnControllerPrefab, transform);
                turnControllers[position].AddListener(controller);
            }
            else
            {
                Debug.Log("已有占位");
            }
        }
    }
    private void Start()
    {
        StartLoop();
    }
    public void StartLoop()
    {
        if (isLooping) return;
        isLooping = true;
        StartCoroutine(TurnCoroutine());
    }

    public void EndCurrent()
    {
        turnControllers[index].EndTurn();
    }

    public void EndPlayerTurn()
    {
        if (isPlayerTurn)
        {
            EndCurrent();
        }
    }

    private IEnumerator TurnCoroutine()
    {
        for (turn = 0; turn < 100; turn++)
        {
            for (index = 0; index < turnControllers.Length; index++)
            {
                isPlayerTurn = index == startIndex;
                var c = turnControllers[index];
                if (c == null) continue;
                c.OnStartTurn();
                yield return new WaitUntil(() => c.EndTurnTrigger);
                c.OnEndTurn();
                if (c.AdditionalTurn) index--;
            }
        }
        if (turn == 100) Debug.LogWarning("回合数达到上限100");
        //var loot = GameManager.Instance.CardLibrary.GetRandom(3);
        //GUISystemManager.Instance.Open("w_select_loot", loot);
        //yield return new WaitUntil(() => ForegoundGUISystem.current == null);
        //GameManager.Instance.CompleteCurrentIncident();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndCurrent();
        }
    }

}
