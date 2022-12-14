namespace WpfApp1;

public class SearchFieldInfo
{
    public SearchFieldInfo(string name, object? value)
    {
        Name = name;
        Value = string.Join(" ", value);
    }
    
    public SearchFieldInfo(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public string Value { get; set; }
}