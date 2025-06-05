public class Data
{
    public int ID;
    public string Title;
    public string Description;

    public Data()
    {

    }

    public Data(string title, string description, int id = 0)
    {
        ID = id;
        Title = title;
        Description = description;
    }
} 