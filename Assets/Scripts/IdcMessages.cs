

public class IdcMessages
{
    string _header, _content;
    public string Header{
        get => _header;
        set => _header = value;
        }

    public string Content
    {
        get => _content;
        set => _content = value;
    }

    public IdcMessages(string header, string content)
    {
        _header = header;
        _content = content;
    }
}
