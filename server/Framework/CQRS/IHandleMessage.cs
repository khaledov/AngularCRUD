namespace Framework.CQRS
{
    public interface IHandleMessage<in T> where T : Message
    {
        void Handle(T message);
    }
}