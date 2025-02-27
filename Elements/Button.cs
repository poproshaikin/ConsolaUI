namespace ConsolaUI;

public class Button : ElementBase, ISelectableElement, IClickableElement
{
    public string Content { get; set; }

    public ConsoleColor OnSelectedBackgroundColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor OnSelectedTextColor { get; set; } = ConsoleColor.Black;
    
    public bool Selected { get; set; }

    public event Action? OnClick;
    public event Action? OnSelected;

    public Button(string content, Action? onClick = null, string? id = null)
    {
        Id = id;
        Content = content;
        TextColor = ConsoleColor.Blue;
        OnClick += onClick;
    }

    public void Toggle() => Selected = !Selected;
    
    public void Click() => OnClick?.Invoke();
    public void Select() => OnSelected?.Invoke();
    
    public override string GetString()
    {
        return Content;
    }

    public override void Print()
    { 
        Console.BackgroundColor = Selected ? OnSelectedBackgroundColor : BackgroundColor;
        Console.ForegroundColor = Selected ? OnSelectedTextColor : TextColor;
        
        Console.Write(GetString());

        Console.ResetColor();
        Console.ResetColor();
    }
}