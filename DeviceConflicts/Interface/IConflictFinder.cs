using DeviceConflicts.Model;

namespace DeviceConflicts.Interface;

public interface IConflictFinder
{
    ICollection<Conflict> FindConflicts();
}