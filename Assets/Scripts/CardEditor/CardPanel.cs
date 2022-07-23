using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
namespace CardEditor
{ 
    public class CardPanel : MonoBehaviour
    {
        [SerializeField]
        protected GameObject prefab;
        [SerializeField]
        protected Transform parent;
        [SerializeField]
        private TextAsset[] xmlFiles;

        public Vector2 startPos;
        public int widthStep;
        public int heightStep;
        public int maxItemNum = 1;
        public int itemPerRow = 1;
        public virtual int ItemCount { get; }
        public int MaxPage { get => ItemCount / maxItemNum; }
        public bool IsFirstPage { get => PageIndex == 0; }
        public bool IsLastPage { get => PageIndex == MaxPage; }
        List<CardInfo> cardInfos;
        [SerializeField]
        private int pageIndex = 0;
        [SerializeField]
        public int PageIndex
        {
            get => pageIndex;
            set
            {
                if (value < 0 || value > MaxPage)
                    Debug.LogError("不合理的页标");
                else if (pageIndex != value)
                {
                    pageIndex = value;
                    RefreshPage();
                }
            }
        }
        [SerializeField]
        private CardCategory category;
        public CardCategory Category
        {
            get => category;
            set
            {
                if (category != value)
                {
                    category = value;
                    pageIndex = 0;
                    RefreshPage();
                }
            }
        }
        public void SearchTable(string key)
        {
            throw new NotImplementedException();
        }
        //public void SearchTable(SpecialSearch search);
        [ContextMenu("刷新")]
        public void RefreshPage()
        {
            Debug.Log("RefreshPage");
            cardInfos = CardReader.Instance.GetCardInfos(xmlFiles);
            DestroyAllChildren();
            List<CardInfo> cards = GetCardInfoByType();
            int index = pageIndex * maxItemNum;
            int count = maxItemNum;
            if (index > cards.Count) return;
            if (index + count > cards.Count)
                count = cards.Count - index;
            List<CardInfo> showCards = cards.GetRange(index, count);
            for (int i = 0; i < showCards.Count; i++)
            {
                CardInfo card = showCards[i];
                CreatePrefab(card, i);
            }
        }
        GameObject CreatePrefab(CardInfo o, int i)
        {
            Vector2 targetPos = new Vector2(startPos.x + widthStep * (i % itemPerRow), startPos.y - heightStep * (i / itemPerRow));
            GameObject cardObject = Instantiate(prefab, parent);
            cardObject.transform.localPosition = targetPos;
            cardObject.GetComponent<CardObject>().CardInfo=o;
            return cardObject;
        }
        private List<CardInfo> GetCardInfoByType()
        {
            return cardInfos.FindAll(c => c.category == category);
        }
        [ContextMenu("清除")]
        public void DestroyAllChildren()
        {
            int cnt = parent.childCount;
            List<Transform> children=new List<Transform>();
            for(int i=0; i < cnt; i++)
            {
                children.Add(parent.GetChild(i));
            }
            foreach(Transform child in children)
                DestroyImmediate(child.gameObject);
        }
    }
}