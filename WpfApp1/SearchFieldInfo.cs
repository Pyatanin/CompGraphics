namespace WpfApp1;

public class SearchFieldInfo
{
    public SearchFieldInfo(string name, string value)
    {
        Name = name;
        Value = value;
    }
    
    public SearchFieldInfo(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public string Value { get; set; } = "";

}