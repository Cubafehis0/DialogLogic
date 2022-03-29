using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Recorder
{
    /// <summary>
    /// ��������־���Թ�����ʹ��
    /// </summary>
    public class CardRecorder : MonoBehaviour
    {

        [SerializeField]
        private ICardOperationSubject subject;

        private List<CardLogEntry> cardLogs = new List<CardLogEntry>();


        public static CardRecorder Instance { get; set; }
        private void Awake()
        {
            Destroy(Instance);
            Instance = this;
        }
        private void OnDisable()
        {
            subject.OnDrawCard.RemoveListener(OnDrawCard);
            subject.OnDiscardCard.RemoveListener(OnDiscardCard);
            subject.OnPlayCard.RemoveListener(OnPlayCard);
        }

        private void OnEnable()
        {
            subject.OnDrawCard.AddListener(OnDrawCard);
            subject.OnDiscardCard.AddListener(OnDiscardCard);
            subject.OnPlayCard.AddListener(OnPlayCard);
        }

        public void OnDrawCard(CardLogEntry entry)
        {
            cardLogs.Add(entry);
        }
        public void OnPlayCard(CardLogEntry entry)
        {
            cardLogs.Add(entry);
        }
        public void OnDiscardCard(CardLogEntry entry)
        {
            cardLogs.Add(entry);
        }

        //ʵ���йز��ҵĹ���
        //���ҵķ�Χ�� ���غ� ���ֶ�ս ��һ�ſ��� �ϻغ�

        public delegate bool Predicate(CardLogEntry log);
        /// <summary>
        /// �Ƿ�����ҵ����ϸ��������Ŀ��Ƽ�¼
        /// </summary>
        /// <param name="scope">���ҵķ�Χ</param>
        /// <param name="currentTurn">��ǰ�غ���</param>
        /// <param name="match">����</param>
        /// <returns></returns>
        public bool CanFind(CardLogFindScopeEnum scope, uint currentTurn, Predicate match)
        {
            return GetCntMeetCdt(scope, currentTurn, match) > 0;
        }
        /// <summary>
        /// �ҵ����ϸ��������Ŀ��Ƽ�¼�ĸ���
        /// </summary>
        /// <param name="scope">���ҵķ�Χ</param>
        /// <param name="currentTurn">��ǰ�غ���</param>
        /// <param name="match">����</param>
        /// <returns></returns>
        public uint GetCntMeetCdt(CardLogFindScopeEnum scope, uint currentTurn, Predicate match)
        {
            IEnumerable<CardLogEntry> res=Query(scope,currentTurn,match);
            return (uint)res.Count();
        }

        public IEnumerable<CardLogEntry> Query(CardLogFindScopeEnum scope, uint currentTurn, Predicate match)
        {
            IEnumerable<CardLogEntry> res;
            switch (scope)
            {
                case CardLogFindScopeEnum.ThisTurn:
                    res = from c in cardLogs
                          where match(c) && c.Turn == currentTurn
                          select c;
                    break;
                case CardLogFindScopeEnum.LastTurn:
                    res = from c in cardLogs
                          where match(c) && c.Turn == currentTurn - 1
                          select c;
                    break;
                case CardLogFindScopeEnum.ThisBattle:
                    res = from c in cardLogs
                          where match(c)
                          select c;
                    break;
                case CardLogFindScopeEnum.LastCard:
                    res = from c in cardLogs
                          where match(c)
                          select c;
                    res = res.Take(1);
                    break;
                default:
                    throw new System.IndexOutOfRangeException();
            }
            return res;
        }
    }
}