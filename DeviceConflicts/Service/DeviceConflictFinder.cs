using DeviceConflicts.Interface;
using DeviceConflicts.Model;

namespace DeviceConflicts.Service;

public class DeviceConflictFinder : IConflictFinder
{
    public ICollection<DeviceInfo> DeviceInfos { get; set; }

    public DeviceConflictFinder(ICollection<DeviceInfo> deviceInfos)
    {
        DeviceInfos = deviceInfos;
    }

    public ICollection<Conflict> FindConflicts()
    {
        var conflicts = DeviceInfos
            .GroupBy(info => info.Brigade.Code)
            .Where(group => group.Any(info => info.Device.IsOnline))
            .Where(group => group.Count() > 1)
            .Select(group => new Conflict
            {
                DevicesSerials = group.Select(info => info.Device.SerialNumber).ToArray(),
                BrigadeCode = group.Key
            })
            .ToList();

        return conflicts;
    }
}