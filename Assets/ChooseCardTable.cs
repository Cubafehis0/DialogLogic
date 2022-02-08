using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IChooseCardTable
{
    /// <summary>
    /// 选择卡牌的界⾯打开
    /// </summary>
    /// <param name="cards">传入card list长度参数应为5</param>
    /// <returns>返回选择的卡片</returns>
    void Open(List<Card> cards);
}
public class ChooseCardTable : MonoBehaviour,IChooseCardTable
{
    [SerializeField]
    private List<Button> chooseCardButtons = null;
    [SerializeField]
    private Button giveUpButton = null;
    [SerializeField]
    private Button confirmButton = null;
    [SerializeField]
    private List<int> chooseCardsNumber = new List<int>();

    private int MAXCARDNUM = 5;
    private int MAXCANCHOOSECARDNUM = 2;
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open(List<Card> cards)
    {
        gameObject.SetActive(true);
        SetCards(cards);

    }

    private void Start()
    {
        Close();
        if (chooseCardButtons.Count == MAXCARDNUM)
        {
            for (int i = 0; i < MAXCARDNUM; i++)
            {
                int temp = i;
                chooseCardButtons[i].onClick.AddListener(()=> OnClickChooseCardButton(temp));
            }
        }
        if (giveUpButton) giveUpButton.onClick.AddListener(OnClickGiveUpButton);
        if (confirmButton) confirmButton.onClick.AddListener(OnClickConfirmButton);
    }
    private void SetCards(List<Card> cards)
    {
        if (cards.Count != 5 && chooseCardButtons.Count != MAXCARDNUM)
        {
            Debug.Log("卡牌或者cardbutton num != " + MAXCARDNUM);
            return;
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                InitCard(cards[i], chooseCardButtons[i]);
            }
        }
    }

    private void InitCard(Card card, Button button)
    {
        GameObject gameObject = button.transform.Find("Canvas").gameObject;
        Text title = gameObject.transform.Find("Title").GetComponent<Text>();
        Text body = gameObject.transform.Find("Body").GetComponent<Text>();
        Text meme = gameObject.transform.Find("Meme").GetComponent<Text>();
        if (title) title.text = card.title;
        if (body) body.text = card.desc;
        if (meme) meme.text = card.meme;
    }

    private void OnClickChooseCardButton(int i)
    {
        Debug.Log("click" + i + "button");
        GameObject gameObject = chooseCardButtons[i].transform.Find("Canvas").gameObject;
        Text title = gameObject.transform.Find("Title").GetComponent<Text>();
        if (chooseCardsNumber.Exists(cardnumber => cardnumber == i))
        {
            chooseCardsNumber.Remove(i);
            title.text = null;
        }
        else if (chooseCardsNumber.Count < MAXCANCHOOSECARDNUM)
        {
            chooseCardsNumber.Add(i);
            title.text = "已选择";
        }
    }
    private void OnClickGiveUpButton()
    {
        chooseCardsNumber.Clear();
    }

    private void OnClickConfirmButton()
    {
        if (chooseCardsNumber.Count <= MAXCANCHOOSECARDNUM)
        {
            Debug.Log("OnClickConfirmButton:"+chooseCardsNumber.Count+"");
            for(int i = 0; i < chooseCardsNumber.Count; i++)
            {
                Debug.Log("chooseCard:"+chooseCardsNumber[i]);
            }
        }
        else
        {
            Debug.Log("选择card数目大于" + MAXCANCHOOSECARDNUM);
        }
        chooseCardsNumber.Clear();
    }

}
