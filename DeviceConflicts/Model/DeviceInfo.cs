namespace DeviceConflicts.Model;

public class DeviceInfo
{
    public Device Device { get; set; }
    public Brigade Brigade { get; set; }

    public override string ToString()
    {
        return $"{nameof(Device)}: {Device}, {nameof(Brigade)}: {Brigade}";
    }
}