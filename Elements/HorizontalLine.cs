namespace ConsolaUI;

public class HorizontalLine : ElementBase
{
    public int Length { get; set; }

    public HorizontalLine(int length)
    {
        Length = length;
    }

    public override string GetString()
    {
        string result = "";

        for (int i = 0; i < Length; i++)
            result += "-";

        return result;
    }
}