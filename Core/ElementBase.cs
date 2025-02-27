namespace ConsolaUI;

public abstract class ElementBase : IElement
{    
    public string? Id { get; set; }
    
    public int PosY { get; set; }
    public int PosX { get; set; }

    public ConsoleColor BackgroundColor { get; set; } 
    public ConsoleColor TextColor { get; set; }

    protected ElementBase()
    {
        BackgroundColor = Colors.DefaultBackgroundColor;
        TextColor = Colors.DefaultTextColor;
    }
    
    public virtual void Print()
    {
        Console.BackgroundColor = BackgroundColor;
        Console.ForegroundColor = TextColor;
        
        Console.Write(GetString());

        Console.ResetColor();
        Console.ResetColor();
    }
    
    public abstract string? GetString();
}