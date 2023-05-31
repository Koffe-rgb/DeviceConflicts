using DeviceConflicts.Interface;
using DeviceConflicts.Model;
using DeviceConflicts.Service;

namespace DeviceConflicts;

public class ProblemSolver
{
    private readonly IProducer<DeviceInfo> _producer;
    private readonly IConsumer<Conflict> _consumer;

    public ProblemSolver(IProducer<DeviceInfo> producer, IConsumer<Conflict> consumer)
    {
        _producer = producer;
        _consumer = consumer;
    }

    public void SolveProblem()
    {
        var deviceInfos = _producer.Produce();

        var conflictFinder = new DeviceConflictFinder(deviceInfos);
        var conflicts = conflictFinder.FindConflicts();

        _consumer.Consume(conflicts);
    }
}

public class App
{
    public static void Main(string[] args)
    {
        const string input = @"..\..\..\Devices.json";
        const string output = @"..\..\..\Conflicts.json";

        var jsonProducer = new JsonProducer(input);
        var jsonConsumer = new JsonConsumer(output);

        var problemSolver = new ProblemSolver(jsonProducer, jsonConsumer);
        problemSolver.SolveProblem();
    }
}