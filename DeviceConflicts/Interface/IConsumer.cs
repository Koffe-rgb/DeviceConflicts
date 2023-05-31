namespace DeviceConflicts.Interface;

public interface IConsumer<T>
{
    void Consume(ICollection<T> collection);
}