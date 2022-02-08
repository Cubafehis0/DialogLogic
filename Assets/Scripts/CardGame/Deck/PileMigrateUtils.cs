using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileMigrateUtils : MonoBehaviour
{
    public static void MigrateTo(Card card, IPile newPile)
    {
        if (card == null || newPile == null)
        {
            Debug.LogError("Migrate Error: ���벻��Ϊ��");
            return;
        }
        if (card.parentPile == newPile)
        {
            Debug.Log("MigrateTo Error: ����ת�Ƶ�����");
            return;
        }
        IPile oldPile = card.parentPile;
        if (oldPile != null) oldPile.Remove(card);
        newPile.Add(card);
    }
}
