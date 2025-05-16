namespace BaseObjects.BaseObject
{

    public class HideoutStationUpgrade
    {
        private int stationId;

        public int StationId
        {
            get => stationId;
            set => stationId = value;
        }

        private int itemId;

        public int ItemId
        {
            get => itemId;
            set => itemId = value;
        }

        private int amount;

        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        private int requiredLevel;

        public int RequiredLevel
        {
            get => requiredLevel;
            set => requiredLevel = value;
        }

        public HideoutStationUpgrade(int stationId, int itemId, int amount, int requiredLevel)
        {
            StationId = stationId;
            ItemId = itemId;
            Amount = amount;
            RequiredLevel = requiredLevel;
        }

        public HideoutStationUpgrade()
        {
        }
    }
}