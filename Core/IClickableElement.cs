namespace ConsolaUI;

public interface IClickableElement : ISelectableElement
{
    event Action? OnClick;

    void Click();
}