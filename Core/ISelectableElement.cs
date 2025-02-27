namespace ConsolaUI;

public interface ISelectableElement : IElement
{
    ConsoleColor OnSelectedBackgroundColor { get; set; } 
    ConsoleColor OnSelectedTextColor { get; set; }

    bool Selected { get; }

    event Action OnSelected;

    void Select();
    void Toggle();
}