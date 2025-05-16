namespace TarkovTrackerBLL.Service;

public class UserQuestItemProgress
{
    private int userId;
    public int UserId { get => userId; set => userId = value; }

    private int questId;
    public int QuestId { get => questId; set => questId = value; }

    private int itemId;
    public int ItemId { get => itemId; set => itemId = value; }

    private int amountCollected;
    public int AmountCollected { get => amountCollected; set => amountCollected = value; }

    public UserQuestItemProgress(int userId, int questId, int itemId, int amountCollected)
    {
        UserId = userId;
        QuestId = questId;
        ItemId = itemId;
        AmountCollected = amountCollected;
    }
}