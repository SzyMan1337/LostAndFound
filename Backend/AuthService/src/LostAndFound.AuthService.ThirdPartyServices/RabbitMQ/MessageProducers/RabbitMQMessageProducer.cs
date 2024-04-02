using LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.Settings;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LostAndFound.AuthService.ThirdPartyServices.RabbitMQ.MessageProducers
{
    public class RabbitMQMessageProducer : IMessageProducer
    {
        private readonly RabbitMQSettings _rabbitMQSettings;

        public RabbitMQMessageProducer(RabbitMQSettings rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings ?? throw new ArgumentNullException(nameof(rabbitMQSettings));
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQSettings.HostName,
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _rabbitMQSettings.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string productDetail = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(productDetail);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish("", _rabbitMQSettings.QueueName, properties, body);
        }
    }
}
