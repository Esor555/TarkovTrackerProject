namespace TTBusinesLogic.BusinesLogic;

public class QuestReward
{
    private int questId;
    public int QuestId { get => questId; set => questId = value; }

    private int experience;
    public int Experience { get => experience; set => experience = value; }

    private int money;
    public int Money { get => money; set => money = value; }

    private float reputation;
    public float Reputation { get => reputation; set => reputation = value; }

    private string items;
    public string Items { get => items; set => items = value; }

    public QuestReward(int questId, int experience, int money, float reputation, string items)
    {
        QuestId = questId;
        Experience = experience;
        Money = money;
        Reputation = reputation;
        Items = items;
    }
}