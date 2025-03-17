namespace ConsolaUI;

public abstract partial class PageBase
{
    protected void Text(string content)
    {
        AddElement(new Text(content));
    }
    
    protected void Header(string content, int margin = 1, char borderChar = '-')
    {
        AddElement(new Header(content, margin, borderChar));
    }
    
    protected void NewLine()
    {
        AddElement(new NewLine());
    }

    protected void Button(string label, Action? onClick = null, string? id = null)
    {
        AddElement(new Button(label, onClick));
    }

    protected void HorizontalLine(int length)
    {
        AddElement(new HorizontalLine(length));
    }

    protected void VerticalLine()
    {
        AddElement(new VerticalLine());
    }

    protected void Margin(int count = 1)
    {
        AddElement(new Margin(count));
    }
    
    protected void InputField(string label, Action<string?>? onInput = null)
    {
        AddElement(new InputField(label, onInput));
    }
}