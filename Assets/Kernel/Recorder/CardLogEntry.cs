
using CardGame.Recorder;
using ModdingAPI;

public class CardLogEntry
{
    private int m_ID;
    private string name;
    private CardType cardCategory;
    private bool isActive;
    private int turn;
    private ActionTypeEnum logType;
    private static int id = 0;

    public int ID { get => m_ID; }
    public int Turn { get => turn; set => turn = value; }
    public ActionTypeEnum LogType { get => logType; set => logType = value; }
    public CardType CardCategory { get => cardCategory; set => cardCategory = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public string Name { get => name; set => name = value; }

    public CardLogEntry()
    {
        m_ID = id++;
    }
}
