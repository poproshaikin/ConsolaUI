namespace ConsolaUI;

public class Text : ElementBase
{
    public string Content { get; set; }

    public Text(string content)
    {
        Content = content;
    }

    public override string? GetString()
    {
        return Content;
    }
}