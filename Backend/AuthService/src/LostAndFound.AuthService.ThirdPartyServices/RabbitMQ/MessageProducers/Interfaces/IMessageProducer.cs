namespace LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.MessageProducers
{
    public interface IMessageProducer
    {
        public void SendMessage<T>(T message);
    }
}
