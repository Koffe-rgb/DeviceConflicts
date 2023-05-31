using DeviceConflicts.Interface;
using DeviceConflicts.Model;
using Newtonsoft.Json;

namespace DeviceConflicts.Service;

public class JsonConsumer : IConsumer<Conflict>
{
    public string OutputFile { get; set; }

    public JsonConsumer(string outputFile)
    {
        OutputFile = outputFile;
    }

    public void Consume(ICollection<Conflict> collection)
    {
        using var writer = new StreamWriter(File.Open(OutputFile, FileMode.OpenOrCreate, FileAccess.Write));
        using var jsonTextWriter = new JsonTextWriter(writer);
        var serializer = new JsonSerializer();
        serializer.Serialize(jsonTextWriter, collection);
        jsonTextWriter.Flush();
    }
}