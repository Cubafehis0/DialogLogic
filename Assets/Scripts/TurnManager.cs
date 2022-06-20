using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int turn = 0;
    [SerializeField]
    private TurnController playerController;
    [SerializeField]
    private TurnController enemyController;
    [SerializeField]
    private bool isPlayerTurn = false;
    [SerializeField]
    private DialogSystem dialogSystem;

    public int Turn { get => turn;}
    public bool IsPlayerTurn { get => isPlayerTurn;}

    private void Start()
    {
        turn = 0;
        StartCoroutine(TurnCoroutine());
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
            Debug.Log("���˻غϽ���");
            enemyController.EndTurn();
            do
            {
                if (dialogSystem.InkStory.NextState == Ink2Unity.InkState.Finish) break;
                isPlayerTurn = true;
                playerController.StartTurn();
                yield return new WaitUntil(() => playerController.EndTurnTrigger);
                Debug.Log("�����غϽ���");
                playerController.EndTurn();
            } while (playerController.AdditionalTurn);

        }
        if (i == 100) Debug.LogWarning("�غ����ﵽ����100");
        var loot = GameManager.Instance.CardLibrary.GetRandom(3);
        GUISystemManager.Instance.Open("w_select_loot", loot);
        yield return new WaitUntil(() => ForegoundGUISystem.current == null);
        GameManager.Instance.CompleteCurrentIncident();
    }

}
