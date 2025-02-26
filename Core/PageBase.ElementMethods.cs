namespace ConsolaUI;

public abstract partial class PageBase
{
    protected void Text(string content)
    {
        AddElement(new Text(content));
    }
    
    protected void NewLine()
    {
        AddElement(new NewLine());
    }

    protected void Button(string label, Action onClick)
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

    protected void Margin(int count)
    {
        AddElement(new Margin(count));
    }
}