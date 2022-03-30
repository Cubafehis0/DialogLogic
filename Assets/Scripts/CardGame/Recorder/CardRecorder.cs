using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Recorder
{
    /// <summary>
    /// 管理卡牌日志，以供查阅使用
    /// </summary>
    public class CardRecorder : MonoBehaviour
    {
        public List<CardLogEntry> cardLogs = new List<CardLogEntry>();
        public static CardRecorder Instance { get; set; }
        private void Awake()
        {
            Destroy(Instance);
            Instance = this;
        }

        public void AddRecordEntry(CardLogEntry entry)
        {
            cardLogs.Add(entry);
        }
    }
}