namespace Framework.CQRS
{
    public interface IEventStore
    {
       void Save<T>(T theEvent) where T : EventBase;
    }
}