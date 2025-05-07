namespace TTBusinesLogic.BusinesLogic;


    public class QuestRequiredItem
    {
        private int questId;
        public int QuestId { get => questId; set => questId = value; }

        private int itemId;
        public int ItemId { get => itemId; set => itemId = value; }

        private int amount;
        public int Amount { get => amount; set => amount = value; }

        public QuestRequiredItem(int questId, int itemId, int amount)
        {
            QuestId = questId;
            ItemId = itemId;
            Amount = amount;
        }
    }
