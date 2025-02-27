namespace ConsolaUI;

public class InputField : Element, IClickableElement
{
    public string Placeholder { get; set; }

    public ConsoleColor OnSelectedBackgroundColor { get; set; } = Colors.DefaultSelectedBackgroundColor;
    public ConsoleColor OnSelectedTextColor { get; set; } = Colors.DefaultSelectedTextColor;
    
    public bool Selected { get; set; }
    
    public event Action? OnSelected;
    public event Action? OnClick;
    public event Action<string?>? OnInput;

    private int _endPosY;
    private int _endPosX;
    
    public InputField(string placeholder, Action<string?>? onInput = null)
    {
        Placeholder = placeholder;
        OnInput += onInput;
    }

    public void Select()
    {
        OnSelected?.Invoke();
    }

    public void Click() 
    {
        OnClick?.Invoke();
        
        if (Selected)
        {
            MoveCursorToInput();
            
            Console.BackgroundColor = Selected ? OnSelectedBackgroundColor : BackgroundColor;
            Console.ForegroundColor = Selected ? OnSelectedTextColor : TextColor;
            
            string? input = Console.ReadLine();
            
            Console.ResetColor();
            Console.ResetColor();
            
            OnInput?.Invoke(input);
        }
    }

    public void Toggle() => Selected = !Selected;
    
    public override string GetString()
    {
        return Placeholder;
    }
    
    public override void Print()
    { 
        Console.BackgroundColor = Selected ? OnSelectedBackgroundColor : BackgroundColor;
        Console.ForegroundColor = Selected ? OnSelectedTextColor : TextColor;
        
        Console.Write(GetString());

        Console.ResetColor();
        Console.ResetColor();
        
        _endPosY = Console.CursorTop;
        _endPosX = Console.CursorLeft;
    }

    private void MoveCursorToInput()
    {
        Console.SetCursorPosition(_endPosX, _endPosY);
    }
}