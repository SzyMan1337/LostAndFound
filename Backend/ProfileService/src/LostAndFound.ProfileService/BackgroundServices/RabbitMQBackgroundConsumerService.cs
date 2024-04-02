using LostAndFound.ProfileService.Core.UserProfileServices.Interfaces;
using LostAndFound.ProfileService.CoreLibrary.Messages;
using LostAndFound.ProfileService.CoreLibrary.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace LostAndFound.ProfileService.BackgroundServices
{
    public class RabbitMQBackgroundConsumerService : BackgroundService
    {
        private IConnection _connection = null!;
        private IModel _channel = null!;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly RabbitMQSettings _rabbitMQSettings;

        public RabbitMQBackgroundConsumerService(IServiceScopeFactory serviceScopeFactory, RabbitMQSettings rabbitMQSettings)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _rabbitMQSettings = rabbitMQSettings ?? throw new ArgumentNullException(nameof(rabbitMQSettings));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (ch, ea) =>
            {
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    var newUserAccount = JsonSerializer.Deserialize<NewUserAccountMessageDto>(message);
                    if (newUserAccount is null)
                    {
                        throw new ArgumentException(nameof(newUserAccount));
                    }

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var userProfileService = scope.ServiceProvider.GetRequiredService<IUserProfileService>();

                        await userProfileService.CreateUserProfile(newUserAccount);
                    }

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }
            };

            _channel.BasicConsume(_rabbitMQSettings.QueueName, false, consumer);

            await Task.CompletedTask;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMQSettings.HostName,
                DispatchConsumersAsync = true,
                Port = _rabbitMQSettings.Port,
            };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMQSettings.QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);

            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
