namespace TarkovTrackerBLL.Service
{

    public class HideoutStation
    {
        private int id;

        public int Id
        {
            get => id;
            set => id = value;
        }

        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public HideoutStation(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}