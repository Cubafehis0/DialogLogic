using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Recorder
{
    /// <summary>
    /// ��������־���Թ�����ʹ��
    /// </summary>
    public class CardRecorder
    {
        public List<CardLogEntry> cardLogs = new List<CardLogEntry>();

        public void AddRecordEntry(CardLogEntry entry)
        {
            cardLogs.Add(entry);
        }

        public int this[string name]
        {
            get
            {
                return name switch
                {
                    "preach_total" => QueryPreachTotal(),
                    "preach_thisturn" => QueryPreachThisTurn(),
                    "activate_count" => QueryTotalActive(),
                    "logic_combo" => QueryCombo(CardCategory.Lgc),
                    "immoral_combo" => QueryCombo(CardCategory.Imm),
                    "spirital_combo" => QueryCombo(CardCategory.Spt),
                    "moral_combo" => QueryCombo(CardCategory.Mrl),
                    _ => throw new CardPlayerState.PropNotFoundException()
                };
            }
        }

        public int QueryTotalActive()
        {
            var qs = from x in cardLogs
                     where x.LogType == ActionTypeEnum.ActivateCard
                     orderby x.ID descending
                     select x;
            return qs.Count();
        }

        public int QueryCombo(CardCategory category)
        {
            var qs = from x in cardLogs
                     where x.LogType == ActionTypeEnum.PlayCard
                     && x.CardCategory == category
                     && x.Turn == CardGameManager.Instance.Turn
                     orderby x.ID descending
                     select x;
            int res = 0;
            if (qs.Count() > 0)
            {
                int index = qs.First().ID;
                foreach (var item in qs)
                {
                    if (item.ID == index)
                    {
                        res++;
                        index--;
                    }
                }
            }
            Debug.Log("Combo:" + res);
            return res;
        }

        public int QueryPreachTotal()
        {
            return (from x in cardLogs
                    where x.Name == "preach"
                    && x.LogType == ActionTypeEnum.PlayCard
                    select x).Count();
        }

        public int QueryPreachThisTurn()
        {
            return (from x in cardLogs
                    where x.Name == "preach"
                && x.LogType == ActionTypeEnum.PlayCard
                && x.Turn == CardGameManager.Instance.Turn
                    select x).Count();
        }

    }
}