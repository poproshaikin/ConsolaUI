namespace ConsolaUI;

public interface IClickableElement
{
    event Action? OnClick;

    void Click();
}