namespace DeviceConflicts.Model;

public class Device
{
    public string SerialNumber { get; set; }
    public bool IsOnline { get; set; }

    public override string ToString()
    {
        return $"{nameof(SerialNumber)}: {SerialNumber}, {nameof(IsOnline)}: {IsOnline}";
    }
}