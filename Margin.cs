namespace ConsolaUI;

public class Margin : Element
{
    public int Length { get; set; }

    public Margin()
    {
        Length = 1;
    }
    
    public Margin(int length)
    {
        Length = length;
    }

    public override string? GetString()
    {
        string result = "";

        for (int i = 0; i < Length; i++)
            result += " ";

        return result;
    }
}