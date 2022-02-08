using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestScript : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject gameObject1;
    [SerializeField]
    private GameObject card1; 
    public IChooseCardTable chooseCardTable;
    private void Start()
    {
        chooseCardTable = gameObject1.GetComponent<IChooseCardTable>();
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        List<Card> cards = new List<Card>();
        Card card = card1.GetComponent<Card>();
        card.desc = "1";
        card.title = "1";
        card.meme = "1";
        for (int i = 0; i < 5; i++)
            cards.Add(card);
        chooseCardTable.Open(cards);
    }
}
