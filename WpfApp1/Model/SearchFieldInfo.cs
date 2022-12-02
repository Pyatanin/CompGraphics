namespace WpfApp1.Model;

public class SearchFieldInfo
{
        public SearchFieldInfo(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public string Value { get; set; } = "hello";
}