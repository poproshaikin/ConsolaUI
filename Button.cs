namespace ConsolaUI;

public class Button : Element
{
    private event Action? _onClick;
    private event Action? _onActive;
    
    public string Content { get; set; }

    public ConsoleColor ActiveBackgroundColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor ActiveTextColor { get; set; } = ConsoleColor.Black;
    
    public bool Active { get; set; }

    public Button(string content, Action? onClick = null, string? id = null)
    {
        Id = id;
        Content = content;
        TextColor = ConsoleColor.Blue;
        _onClick += onClick;
    }

    public void Toggle() => Active = !Active;

    public void OnClick(Action onClick) => _onClick += onClick;
    public void OnActive(Action onActive) => _onActive += onActive;
    
    public void InvokeClick() => _onClick?.Invoke();
    public void InvokeActive() => _onActive?.Invoke();
    
    public override string GetString()
    {
        return Content;
    }

    public override void Print()
    { 
        Console.BackgroundColor = Active ? ActiveBackgroundColor : BackgroundColor;
        Console.ForegroundColor = Active ? ActiveTextColor : TextColor;
        
        Console.Write(GetString());

        Console.ResetColor();
        Console.ResetColor();
    }
}