namespace BaseObjects.BaseObject;

public class Item
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
}