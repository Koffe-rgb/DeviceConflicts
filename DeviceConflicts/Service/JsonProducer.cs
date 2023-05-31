using DeviceConflicts.Interface;
using DeviceConflicts.Model;
using Newtonsoft.Json;

namespace DeviceConflicts.Service;

public class JsonProducer : IProducer<DeviceInfo>
{
    public string InputFile { get; set; }

    public JsonProducer(string inputFile)
    {
        InputFile = inputFile;
    }

    public ICollection<DeviceInfo> Produce()
    {
        ICollection<DeviceInfo>? deviceInfos;
        
        using var reader = new StreamReader(File.Open(InputFile, FileMode.Open, FileAccess.Read));
        using (var jsonTextReader = new JsonTextReader(reader))
        {
            var serializer = new JsonSerializer();
            deviceInfos = serializer.Deserialize<ICollection<DeviceInfo>>(jsonTextReader);
        }

        if (deviceInfos == null)
        {
            throw new Exception("Couldn't deserialize objects");
        }

        return deviceInfos;
    }
}