public class Cat : BaseModal
{
    private string url;
    private string name;

    private uint age;

    public string Id { get => id; set => id = value; }
    public string Url { get => url; set => url = value; }
    public string Name { get => name; set => name = value; }
    public uint Age { get => age; set => age = value; }

    public override string ToString()

    {
        string name = GetType().FullName?.ToString() ?? "";
        return $"{name}\n\tid = {id}\n\tname = {name}\n\turl = {url}";
    }
}
