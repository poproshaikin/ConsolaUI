namespace ConsolaUI;

public class Header : ElementBase
{
    public string Content { get; }

    private int _margin;
    private char _borderChar;
    
    public Header(string content, int margin = 1, char borderChar = '-')
    {
        Content = content;
        _margin = margin;
        _borderChar = borderChar;
    }
    
    public override string? GetString()
    {
        string border = new string(_borderChar, _margin);
        return $"{border} \x1b[1m{Content}\x1b[0m {border}";
    }
}