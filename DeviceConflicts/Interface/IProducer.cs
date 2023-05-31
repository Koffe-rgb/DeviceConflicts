namespace DeviceConflicts.Interface;

public interface IProducer<T>
{
    ICollection<T> Produce();
}