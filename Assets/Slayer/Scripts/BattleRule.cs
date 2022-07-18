using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRule : MonoBehaviour
{
    private bool isInBattle = false;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private List<string> enemys;
    [SerializeField]
    private List<Transform> spawnPoints;
    public void StartBattle(IEnumerable<string> defenders)
    {
        if (isInBattle) return;
        isInBattle = true;
        TurnManager.Instance.RegisterController(player, 0);
        int index = 4;
        foreach (var defender in defenders)
        {
            var t = EnemyLibrary.Instance.GetEnemy(defender);
            t.transform.position = spawnPoints[enemys.IndexOf(defender)].position;
            TurnManager.Instance.RegisterController(t.gameObject, index);
        }
        TurnManager.Instance.StartLoop();
    }

    private void Start()
    {
        StartBattle(enemys);
    }
}
