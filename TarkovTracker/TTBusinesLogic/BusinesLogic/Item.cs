namespace TTBusinesLogic.BusinesLogic;

public partial class Item
{
    private int id;
    public int Id { get => id; set => id = value; }

    private string name;
    public string Name { get => name; set => name = value; }

    private string type;
    public string Type { get => type; set => type = value; }

    public Item(int id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

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

    public class HideoutStation
    {
        private int id;
        public int Id { get => id; set => id = value; }

        private string name;
        public string Name { get => name; set => name = value; }

        public HideoutStation(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class HideoutStationUpgrade
    {
        private int stationId;
        public int StationId { get => stationId; set => stationId = value; }

        private int itemId;
        public int ItemId { get => itemId; set => itemId = value; }

        private int amount;
        public int Amount { get => amount; set => amount = value; }

        private int requiredLevel;
        public int RequiredLevel { get => requiredLevel; set => requiredLevel = value; }

        public HideoutStationUpgrade(int stationId, int itemId, int amount, int requiredLevel)
        {
            StationId = stationId;
            ItemId = itemId;
            Amount = amount;
            RequiredLevel = requiredLevel;
        }
    }

    public class UserHideout
    {
        private int userId;
        public int UserId { get => userId; set => userId = value; }

        private int stationId;
        public int StationId { get => stationId; set => stationId = value; }

        private int stationLevel;
        public int StationLevel { get => stationLevel; set => stationLevel = value; }

        public UserHideout(int userId, int stationId, int stationLevel)
        {
            UserId = userId;
            StationId = stationId;
            StationLevel = stationLevel;
        }
    }
}