
namespace Framework.CQRS
{
    public class Message
    {
        public int SagaId { get; protected set; }
        public string Name { get; protected set; }
    }
}