namespace BaseObjects.BaseObject;

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