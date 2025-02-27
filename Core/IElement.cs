namespace ConsolaUI;

public interface IElement
{
    string? Id { get;  }
    
    int PosY { get; }
    int PosX { get; }

    ConsoleColor BackgroundColor { get; } 
    ConsoleColor TextColor { get; }

    void Print();
    string? GetString();
}