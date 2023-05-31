using DeviceConflicts.Model;
using DeviceConflicts.Service;

namespace DeviceConflicts.Test.Service;

public class DeviceConflictFinderTest
{
    private readonly Brigade _brigade1 = new() { Code = "Code1" };
    private readonly Brigade _brigade2 = new() { Code = "Code2" };
    private readonly Brigade _brigade3 = new() { Code = "Code3" };

    private readonly Device _device1 = new() { SerialNumber = "SN1", IsOnline = false };
    private readonly Device _device2 = new() { SerialNumber = "SN2", IsOnline = true };
    private readonly Device _device3 = new() { SerialNumber = "SN3", IsOnline = false };
    private readonly Device _device4 = new() { SerialNumber = "SN4", IsOnline = true };

    [Fact]
    public void FindConflicts_Returns_EmptyCollection_When_NoConflictExists()
    {
        // Arrange
        var deviceInfos = new List<DeviceInfo>
        {
            new() { Device = _device1, Brigade = _brigade1 },
            new() { Device = _device2, Brigade = _brigade2 },
            new() { Device = _device3, Brigade = _brigade1 },
            new() { Device = _device4, Brigade = _brigade3 }
        };

        var conflictFinder = new DeviceConflictFinder(deviceInfos);

        // Act
        var conflicts = conflictFinder.FindConflicts();

        // Assert
        Assert.Empty(conflicts);
    }

    [Fact]
    public void FindConflicts_Returns_CollectionWithConflict_When_Conflict_Exists()
    {
        // Arrange
        var deviceInfos = new List<DeviceInfo>
        {
            new() { Device = _device1, Brigade = _brigade1 },
            new() { Device = _device2, Brigade = _brigade1 },
            new() { Device = _device3, Brigade = _brigade2 },
            new() { Device = _device4, Brigade = _brigade3 }
        };

        var conflictFinder = new DeviceConflictFinder(deviceInfos);

        // Act
        var conflicts = conflictFinder.FindConflicts();

        // Assert
        Assert.NotEmpty(conflicts);
        Assert.Equal(1, conflicts.Count);
    }

    [Fact]
    public void FindConflicts_Returns_EmptyCollection_When_AllDevicesAreOffline()
    {
        // Arrange
        var deviceInfos = new List<DeviceInfo>
        {
            new() { Device = _device1, Brigade = _brigade1 },
            new() { Device = _device3, Brigade = _brigade2 }
        };
        
        var conflictFinder = new DeviceConflictFinder(deviceInfos);

        // Act
        var conflicts = conflictFinder.FindConflicts();

        // Assert
        Assert.Empty(conflicts);
    }
    
    [Fact]
    public void FindConflicts_Returns_EmptyCollection_When_GroupConsistsOfOneDevice()
    {
        // Arrange
        var deviceInfos = new List<DeviceInfo>
        {
            new() { Device = _device1, Brigade = _brigade1 },
            new() { Device = _device3, Brigade = _brigade2 },
            new() { Device = _device4, Brigade = _brigade3 }
        };
        
        var conflictFinder = new DeviceConflictFinder(deviceInfos);

        // Act
        var conflicts = conflictFinder.FindConflicts();

        // Assert
        Assert.Empty(conflicts);
    }

    [Fact]
    public void FindConflicts_Returns_CollectionWithConflict_When_TheresAtLeastOneOnlineDeviceInGroup()
    {
        // Arrange
        var deviceInfos = new List<DeviceInfo>
        {
            new() { Device = _device1, Brigade = _brigade1 },
            new() { Device = _device2, Brigade = _brigade1 },
            new() { Device = _device3, Brigade = _brigade1 },
            new() { Device = _device4, Brigade = _brigade3 }
        };

        var conflictFinder = new DeviceConflictFinder(deviceInfos);

        // Act
        var conflicts = conflictFinder.FindConflicts();

        // Assert
        Assert.NotEmpty(conflicts);
        Assert.Equal(1, conflicts.Count);
        Assert.Equal(
            conflicts.ToList()[0].DevicesSerials, 
            new []{_device1.SerialNumber, _device2.SerialNumber, _device3.SerialNumber}
            );
    }

    [Fact]
    public void FindConflicts_Returns_EmptyCollection_When_DeviceInfosIsEmpty()
    {
        // Arrange
        var deviceInfos = new List<DeviceInfo>();

        var conflictFinder = new DeviceConflictFinder(deviceInfos);

        // Act
        var conflicts = conflictFinder.FindConflicts();

        // Assert
        Assert.Empty(conflicts);
    }
}