namespace DeviceConflicts.Model;

public class Brigade
{
    public string Code { get; set; }

    public override string ToString()
    {
        return $"{nameof(Code)}: {Code}";
    }
}