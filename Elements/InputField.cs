namespace ConsolaUI;

public class InputField : Element
{
    public string Placeholder { get; set; }
    
    public InputField(string placeholder)
    {
        Placeholder = placeholder;
    }
    
    public override string GetString()
    {
        return Placeholder;
    }
}