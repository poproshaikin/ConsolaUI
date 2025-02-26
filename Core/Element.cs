namespace ConsolaUI;

public abstract class Element
{
    public string? Id { get; set; }
    
    public int PosY { get; set; }
    public int PosX { get; set; }

    public ConsoleColor BackgroundColor { get; set; } 
    public ConsoleColor TextColor { get; set; }
    
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